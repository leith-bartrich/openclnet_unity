using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Collections.Generic;
using ExtraConstraints;

namespace OpenCL.Net
{

	/// <summary>
	/// Apple CGL context.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct AppleCGLContext : IHandle, IHandleData
	{
		private readonly IntPtr _handle;
		
		internal AppleCGLContext(IntPtr handle)
		{
			_handle = handle;
		}
		
		#region IHandleData Members
		
		IntPtr IHandleData.Handle
		{
			get
			{
				return _handle;
			}
		}
		
		#endregion
		


		public static readonly AppleCGLContext Zero = new AppleCGLContext(IntPtr.Zero);
	}


	/// <summary>
	/// Apple share group.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct AppleShareGroup : IHandle, IHandleData
	{
		private readonly IntPtr _handle;
		
		internal AppleShareGroup(IntPtr handle)
		{
			_handle = handle;
		}
		
		#region IHandleData Members
		
		IntPtr IHandleData.Handle
		{
			get
			{
				return _handle;
			}
		}
		
		#endregion
		


		public static readonly AppleShareGroup Zero = new AppleShareGroup(IntPtr.Zero);
	}

	/// <summary>
	/// Windows WGL context.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct WGLContext : IHandle, IHandleData
	{
		private readonly IntPtr _handle;
		
		internal WGLContext(IntPtr handle)
		{
			_handle = handle;
		}
		
		#region IHandleData Members
		
		IntPtr IHandleData.Handle
		{
			get
			{
				return _handle;
			}
		}
		
		#endregion
		
		
		
		public static readonly WGLContext Zero = new WGLContext(IntPtr.Zero);
	}

	/// <summary>
	/// Windows WGL device context.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct WGLDeviceContext : IHandle, IHandleData
	{
		private readonly IntPtr _handle;
		
		internal WGLDeviceContext(IntPtr handle)
		{
			_handle = handle;
		}
		
		#region IHandleData Members
		
		IntPtr IHandleData.Handle
		{
			get
			{
				return _handle;
			}
		}
		
		#endregion
		
		
		
		public static readonly WGLDeviceContext Zero  = new WGLDeviceContext(IntPtr.Zero);
	}

	/// <summary>
	/// Linux GLX context.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct GLXContext : IHandle, IHandleData
	{
		private readonly IntPtr _handle;
		
		internal GLXContext(IntPtr handle)
		{
			_handle = handle;
		}
		
		#region IHandleData Members
		
		IntPtr IHandleData.Handle
		{
			get
			{
				return _handle;
			}
		}
		
		#endregion
		
		
		
		public static readonly GLXContext Zero = new GLXContext(IntPtr.Zero);
	}

	/// <summary>
	/// Linux GLX display.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct GLXDisplay : IHandle, IHandleData
	{
		private readonly IntPtr _handle;
		
		internal GLXDisplay(IntPtr handle)
		{
			_handle = handle;
		}
		
		#region IHandleData Members
		
		IntPtr IHandleData.Handle
		{
			get
			{
				return _handle;
			}
		}
		
		#endregion
		
		
		
		public static readonly GLXDisplay Zero = new GLXDisplay(IntPtr.Zero);
	}


}
