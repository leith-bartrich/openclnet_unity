using System;
using OpenCL.Net;

public static class PlatformExtensions
{
	public static string Name(this Platform self){
		ErrorCode error;
		var inf = Cl.GetPlatformInfo(self,PlatformInfo.Name,out error);
		UnityCL.CheckThrowError(error);
		return inf.ToString();
	}
}

