using System;

namespace OpenCL.Net
{
	/// <summary>
	/// cl_gl_ context info.  part of cl_gl extensions
	/// </summary>
	public enum cl_gl_ContextInfo : uint {
		CL_CURRENT_DEVICE_FOR_GL_CONTEXT_KHR = 0x2006,
		CL_DEVICES_FOR_GL_CONTEXT_KHR = 0x2007,
	};

	/// <summary>
	/// cl_gl_ context properties.  part of cl_gl extensions
	/// </summary>
	public enum cl_gl_ContextProperties : uint {
		CL_GL_CONTEXT_KHR = 0x2008,
		CL_EGL_DISPLAY_KHR = 0x2009,
		CL_GLX_DISPLAY_KHR = 0x200A,
		CL_WGL_HDC_KHR = 0x200B,
		CL_CGL_SHAREGROUP_KHR = 0x200C,
	};

	/// <summary>
	/// cl_gl_khr_ error code.  Extra error_code defined by cl_gl extensions
	/// In practice some drivers throw this for any cl_gl error, regardless of
	/// if the error has anything to do with sharegroups.  So it may be best
	/// to consider this somethign like CL_GL_GENERAL_ERROR_KHR
	/// </summary>
	public enum cl_gl_khr_ErrorCode : int
	{
		CL_INVALID_GL_SHAREGROUP_REFERENCE_KHR = -1000,
	}

	/// <summary>
	/// CL Error codes defined by Apple.
	/// </summary>
	public enum AppleGCLErrorCode : int // cl_int
	{
		NoError = 0,
		BadAttribute = 10000,
		BadProperty = 10001,
		BadPixelFormat = 10002,
		BadRendererInfo = 10003,
		BadContext = 10004,
		BadDrawable = 10005,
		BadDisplay = 10006,
		BadState = 10007,
		BadValue = 10008,
		BadMatch = 10009,
		BadEnumeration = 10010,
		BadOffScreen = 10011,
		BadFullScreen = 10012,
		BadWindow = 10013,
		BadAddress = 10014,
		BadCodeModule = 10015,
		BadAlloc = 10016,
		BadConnection = 10017,
	};

	/// <summary>
	/// Texture target.
	/// Collected values from various header files.  May need updating.
	/// There may be some confusion over deprecation, or platform division here.
	/// </summary>
	public enum TextureTarget : int
	{
		GL_TEXTURE_1D = 0x0DE0,
		GL_TEXTURE_2D = 0x0DE1,
		GL_PROXY_TEXTURE_1D = 0x8063,
		GL_PROXY_TEXTURE_2D = 0x8064,
		GL_TEXTURE_3D_EXT = 0x806F,
		GL_PROXY_TEXTURE_3D_EXT = 0x8070,
	}


}
