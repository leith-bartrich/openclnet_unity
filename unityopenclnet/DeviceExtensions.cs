using System;
using OpenCL.Net;
public static class DeviceExtensions
{
	public static string Name(this Device self){
		ErrorCode error;
		var info = Cl.GetDeviceInfo(self,DeviceInfo.Name, out error);
		if (UnityCL.IsError(error)){
			throw new UnityCLException(error);
		}
		return info.ToString();
	}

	public static DeviceType DeviceType(this Device self){
		ErrorCode error;
		var inf = Cl.GetDeviceInfo(self,DeviceInfo.Type,out error);
		if (UnityCL.IsError(error)){
			throw new UnityCLException(error);
		}
		return inf.CastTo<DeviceType>();

	}

}

