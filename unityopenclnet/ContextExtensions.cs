using System;
using OpenCL.Net;

public static class ContextExtensions
{
	public static Device[] GetDevices(this Context self){
		OpenCL.Net.ErrorCode error;
		var numDevicesInfo = OpenCL.Net.Cl.GetContextInfo(self, ContextInfo.DeviceCount,out error); 
		if (UnityCL.IsError(error)){
			throw new UnityCLException(error);
		}
		var numDevices = numDevicesInfo.CastTo<int>();
		var devicesInfo = OpenCL.Net.Cl.GetContextInfo(self, ContextInfo.Devices,out error);
		if (UnityCL.IsError(error)){
			throw new UnityCLException(error);
		}
		var devices = devicesInfo.CastToArray<Device>(numDevices);
		return devices;
	}
}

