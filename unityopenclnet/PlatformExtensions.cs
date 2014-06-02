using System;
using OpenCL.Net;

public static class PlatformExtensions
{
	public static string Name(this Platform self){
		ErrorCode error;
		var inf = Cl.GetPlatformInfo(self,PlatformInfo.Name,out error);
		if (UnityCL.IsError(error)){
			throw new UnityCLException(error);
		}
		return inf.ToString();
	}
}

