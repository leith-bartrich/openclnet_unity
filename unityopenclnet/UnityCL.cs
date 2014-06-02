using System;

/// <summary>
/// Unity OpenCL Convenience functions
/// </summary>
public static class UnityCL
{
	/// <summary>
	/// Checks if the returned OpenCL error is an error, or is "OK"
	/// </summary>
	/// <returns><c>true</c> if is error is an error; otherwise, <c>false</c>.</returns>
	/// <param name="error">OpenCL Error... which might not be an error.</param>
	public static bool IsError(OpenCL.Net.ErrorCode error){
		return error != OpenCL.Net.ErrorCode.Success;
	}

	/// <summary>
	/// Gets a text description of the error.
	/// Tries to get extension specific error messages where possible.
	/// </summary>
	/// <returns>Description</returns>
	/// <param name="error">Error</param>
	public static string ErrorText(OpenCL.Net.ErrorCode error){
		if (Enum.IsDefined (typeof(OpenCL.Net.ErrorCode), error)) {
			return Enum.GetName (typeof(OpenCL.Net.ErrorCode), error);
		} else if (Enum.IsDefined (typeof(OpenCL.Net.AppleGCLErrorCode),(int) error)) {
			return Enum.GetName (typeof (OpenCL.Net.AppleGCLErrorCode), (int) error);
     	} else if (Enum.IsDefined (typeof(OpenCL.Net.cl_gl_khr_ErrorCode),(int) error)){
			return Enum.GetName (typeof(OpenCL.Net.cl_gl_khr_ErrorCode), (int) error);
		} else {
			return "Unknown OpenCL Error: " + ((int) error).ToString();
		}

	}

}

