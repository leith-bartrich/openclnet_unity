using System;
using System.Runtime.InteropServices;
using System.Linq;

/*
 * This file extends the partial Cl class with all the bits that are relevant to Unity
 * on its many platforms.
 * 
 * At some point, it may make sense to split some of this functionality out into actual proper
 * extension files.  but for now, they all live in here.
 * 
 * We use preprocessor directives to switch the platform code in and out.  Unity defines
 * sepcific variables based on current built settings.
 */

namespace OpenCL.Net
{
	//sometimes requires by code we base on other OpenCL.Net code
	using cl_uint = UInt32;

	public static partial class  Cl
	{

		/// <summary>
		/// Creates the CL image from GL texture.  This can be a platfor specific operation.
		/// On some platforms, some of the parameters may be ignored.
		/// </summary>
		/// <returns>The CL image from GL texture.</returns>
		/// <param name="context">Context.</param>
		/// <param name="memFlags">Mem flags.</param>
		/// <param name="target">Target.</param>
		/// <param name="mipLevel">Mip level.</param>
		/// <param name="texturePointer">Texture pointer.</param>
		/// <param name="error">Error.</param>
		public static IMem CreateCLImageFromGLTexture(Context context, MemFlags memFlags, TextureTarget target, int mipLevel, IntPtr texturePointer, out ErrorCode error){
			#if UNITY_STANDALONE_OSX || UNITY_IPHONE
			//here, we use a function from apple (gcl) headers
			var ret = new Mem(gcl_gl_create_image_from_texture(target,new IntPtr(mipLevel),texturePointer));
			error = ErrorCode.Success;
			return ret;
			#endif
			#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_ANDROID
			//here we use the GL sharing extensions.
			return new Mem(clCreateFromGLTexture2D((context as IHandleData).Handle,memFlags,target,new IntPtr(mipLevel),texturePointer,out error));
			#endif
		}

		//alternative context creation with usuable properties parameter
		[DllImport(Library, EntryPoint="clCreateContext")]
		private static extern IntPtr clCreateContext([In] [MarshalAs(UnmanagedType.LPArray)] cl_uint[] properties,
		                                             cl_uint numDevices,
		                                             [In] [MarshalAs(UnmanagedType.LPArray)] Device[] devices,
		                                             ContextNotify pfnNotify,
		                                             IntPtr userData,
		                                             out ErrorCode errcodeRet);

		#region CLGLExtensions
		[DllImport(Library)]
		private static extern IntPtr clCreateContextFromType([In] [MarshalAs(UnmanagedType.LPArray)] cl_uint[] properties,
		                                                     DeviceType deviceType,
		                                                     ContextNotify pfnNotify,
		                                                     IntPtr userData,
		                                                     [Out] [MarshalAs(UnmanagedType.I4)] out ErrorCode errcodeRet);



		[DllImport(Library)]
		private static extern ErrorCode clEnqueueAcquireGLObjects (CommandQueue queue,
		                                                     uint num_objects,
		                                                     [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] memObjects,
		                                                     uint num_waitlist,
		                                                     [In] [MarshalAs(UnmanagedType.LPArray)] OpenCL.Net.Event[] waitList,
		                                                        out Event ev);

		public static ErrorCode EnqueueAcquireGLObjects(CommandQueue queue, IMem[] glObjects, uint waitListCount, OpenCL.Net.Event[] waitList, out OpenCL.Net.Event outEvent){
			return clEnqueueAcquireGLObjects (queue, (uint)glObjects.Length, (from m in glObjects select (m as IHandleData).Handle).ToArray (), waitListCount, waitList, out outEvent);
		}
		                                                     
		[DllImport(Library)]
		private static extern ErrorCode clEnqueueReleaseGLObjects  (CommandQueue queue,
		                                                           uint num_objects,
		                                                           [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] memObjects,
		                                                           uint num_waitlist,
		                                                           [In] [MarshalAs(UnmanagedType.LPArray)] OpenCL.Net.Event[] waitList,
		                                                           out Event ev);
		public static ErrorCode EnqueueReleaseGLObjects(CommandQueue queue, IMem[] glObjects, uint waitListCount, OpenCL.Net.Event[] waitList, out OpenCL.Net.Event outEvent){
			return clEnqueueReleaseGLObjects (queue, (uint)glObjects.Length, (from m in glObjects select (m as IHandleData).Handle).ToArray (), waitListCount, waitList, out outEvent);
		}

		#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_ANDROID
		[DllImport(Library)]
		private static extern IntPtr clCreateFromGLTexture(IntPtr context, MemFlags memflags, TextureTarget target, IntPtr mip_level, IntPtr texture, out ErrorCode error);
		[DllImport(Library)]
		private static extern IntPtr clCreateFromGLTexture2D(IntPtr context, MemFlags memflags, TextureTarget target, IntPtr mip_level, IntPtr texture, out ErrorCode error);
		[DllImport(Library)]
		private static extern IntPtr clCreateFromGLTexture3D(IntPtr context, MemFlags memflags, TextureTarget target, IntPtr mip_level, IntPtr texture, out ErrorCode error);

		[DllImport(Library)]
		private static extern ErrorCode clGetGLContextInfoKHR(
			[In] [MarshalAs(UnmanagedType.LPArray)] cl_uint[] properties,
			cl_gl_ContextInfo info,
			cl_uint size,
			[Out] [MarshalAs(UnmanagedType.LPArray)] Device[] devices,
			[Out] out cl_uint retSize);
		#endif

		#endregion

		#region OSX


		#if UNITY_STANDALONE_OSX || UNITY_IPHONE

		//library locations
		public const string Library = "/System/Library/Frameworks/OpenCL.framework/Versions/Current/OpenCL";
		public const string GLLibrary = "/System/Library/Frameworks/OpenGL.framework/Versions/Current/OpenGL";
		
		//CREATE CGL Context
		[DllImport(GLLibrary)]
		private static extern IntPtr CGLGetCurrentContext();
		
		public static AppleCGLContext AppleGetCurrentCGLContext()
		{
			return new AppleCGLContext(CGLGetCurrentContext());
		}
		
		//RELEASE CGL Context
		[DllImport(GLLibrary)]
		public static extern void CGLReleaseContext(IntPtr context);
		public static void AppleReleaseCGLContext(AppleCGLContext context)
		{
			CGLReleaseContext((context  as IHandleData).Handle);
		}

		//Get CL Context
		[DllImport(Library)]
		private static extern IntPtr gcl_get_context();
		public static Context AppleGetCLContext()
		{
			return new Context(gcl_get_context());
		}
		
		//GET SHARE GROUP
		[DllImport(GLLibrary)]
		private static extern IntPtr CGLGetShareGroup(IntPtr context);
		
		public static AppleShareGroup AppleGetShareGroup(AppleCGLContext context)
		{
			return new AppleShareGroup(CGLGetShareGroup((context  as IHandleData).Handle));
		}
		
		//SET SHARE GROUP
		[DllImport(Library)]
		private static extern void gcl_gl_set_sharegroup(IntPtr shareGroup);
		
		public static void AppleSetCLShareGroup(AppleShareGroup shareGroup)
		{
			gcl_gl_set_sharegroup((shareGroup  as IHandleData).Handle);
		}


		/// <summary>
		/// Executes full logic for getting CL Context on Apple platforms.
		/// </summary>
		/// <returns>The get CL shared context.</returns>
		public static Context AppleGetCLSharedContext(){
			var cglconext = AppleGetCurrentCGLContext();
			if ((cglconext as IHandleData).Handle == IntPtr.Zero){
				throw new UnityCLException("Attempt to get a shared CGL Context failed.  Returned null");
			}
			var shareGroup = AppleGetShareGroup(cglconext);
			AppleSetCLShareGroup(shareGroup);
			return AppleGetCLContext();
		}

		//Apple specific image creation from GL texture.
		[DllImport(Library)]
		private static extern IntPtr gcl_gl_create_image_from_texture(TextureTarget target, IntPtr mip_level, IntPtr texture);

		#endif

		#endregion

		#region Windows


		#if UNITY_STANDALONE_WIN

		//library locations. Windows will eventually find them in the right system dir this way.
		public const string Library = "OpenCL.dll";
		public const string GLLibrary = "opengl32.dll";

		/// <summary>
		/// Executes full logic for getting CL Context on Windows platforms.
		/// </summary>
		/// <returns>The get CL shared context.</returns>
		public static Context WindowsGetCLSharedContext(Platform p){

			ErrorCode error;
			var wglContext = WindowsGetCurrentWGLContext();
			if ((wglContext as IHandleData).Handle.ToInt32() == 0) {
				throw new UnityCLException("wgl context was null.");
			}
			var wglDeviceContext = WindowsGetCurrentWGLDeviceContext();
			if ((wglDeviceContext as IHandleData).Handle.ToInt32() == 0) {
				throw new UnityCLException("wgl device context was null.");
			}
			uint[] properties = new uint[]{ (uint)cl_gl_ContextProperties.CL_GL_CONTEXT_KHR, (uint) (wglContext as IHandleData).Handle.ToInt32(), (uint) cl_gl_ContextProperties.CL_WGL_HDC_KHR, (uint) (wglDeviceContext as IHandleData).Handle.ToInt32(), (uint) ContextProperties.Platform,(uint) (p as IHandleData).Handle.ToInt32(),0};
			var context = new Context(clCreateContextFromType(properties,DeviceType.All,null,IntPtr.Zero,out error));
			if (UnityCL.IsError(error)){
				throw new UnityCLException(error);
			}
			if ((context as IHandleData).Handle.ToInt32 () == 0) {
				throw new UnityCLException("OpenCL context was null.");
			}
			return context;
		}


		//Get WGL Context (windows GL context)
		[DllImport(GLLibrary)]
		private static extern IntPtr wglGetCurrentContext();
		
		public static WGLContext WindowsGetCurrentWGLContext()
		{
			return new WGLContext(wglGetCurrentContext());
		}

		//Get WGL Device Context (windows GL context)
		[DllImport(GLLibrary)]
		private static extern IntPtr wglGetCurrentDC();
		
		public static WGLDeviceContext WindowsGetCurrentWGLDeviceContext()
		{
			return new WGLDeviceContext(wglGetCurrentDC());
		}

		#endif

		#endregion

		#region Linux
		#if UNITY_STANDALONE_LINUX

		/// <summary>
		/// Executes full logic for getting CL Context on Linux platforms.
		/// Untested.  May need updating to work like windows version.
		/// </summary>
		/// <returns>The get CL shared context.</returns>
		public static Context LinuxGetCLSharedContext(Platform p){
			ErrorCode error;
			var glxContext = LinuxGetCurrentGLXContext();
			var glxDisplay = LinuxGetCurrentGLXDisplay();
			uint[] properties = new uint[]{ (uint)cl_gl_ContextProperties.CL_GL_CONTEXT_KHR, (uint) (glxContext as IHandleData).Handle.ToInt32(), (uint) cl_gl_ContextProperties.CL_GLX_DISPLAY_KHR, (uint) (glxDisplay as IHandleData).Handle.ToInt32(), (uint) ContextProperties.Platform,(uint) (p as IHandleData).Handle.ToInt32(),0};
			Device[] devices;
			uint retSize;
			error = clGetGLContextInfoKHR(properties,cl_gl_ContextInfo.CL_DEVICES_FOR_GL_CONTEXT_KHR,32,out devices,out retSize);
			if (UnityCL.IsError(error)){
				throw new UnityCLException(error);
			}
			var context = new Context(clCreateContext(properties,(uint) devices.Length,devices,null,IntPtr.Zero,out error));
			if (UnityCL.IsError(error)){
				throw new UnityCLException(error);
			}
			return context;
		}

		//Get GLX Context (linux/unix GL context)
		[DllImport(GLLibrary)]
		private static extern IntPtr glXGetCurrentContext();
		
		public static GLXContext LinuxGetCurrentGLXContext()
		{
			return new GLXContext(glXGetCurrentContext());
		}
		
		//Get GLX Display (linux/unix GL display)
		[DllImport(GLLibrary)]
		private static extern IntPtr glXGetCurrentDisplay();
		
		public static GLXDisplay LinuxGetCurrentGLXDisplay()
		{
			return new GLXDisplay(glXGetCurrentDisplay());
		}

		#endif

		#endregion

	}
}

