using System;
public static class UnityCL
{
	public static bool IsError(OpenCL.Net.ErrorCode error){
		return error != OpenCL.Net.ErrorCode.Success;
	}

	public static string ErrorText(OpenCL.Net.ErrorCode error){
		return Enum.GetName(typeof(OpenCL.Net.ErrorCode),error);
	}

	public static void CheckThrowError(OpenCL.Net.ErrorCode error){
		if (IsError(error)){
			throw new UnityCLException("OpenCL Error: " + ErrorText(error));
		}
	}
}

