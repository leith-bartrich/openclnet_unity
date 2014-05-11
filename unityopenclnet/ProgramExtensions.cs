using System;
using OpenCL.Net;
public static class ProgramExtensions
{

	public static string GetBuildError(this Program self){
		var devices = self.GetDevices();
		OpenCL.Net.ErrorCode logerror;
		var log = OpenCL.Net.Cl.GetProgramBuildInfo(self,devices[0],OpenCL.Net.ProgramBuildInfo.Log, out logerror);
		if (UnityCL.IsError(ErrorCode)){
			throw new UnityCLException(logerror);
		}
		return log.ToString();
	}

	public static Device[] GetDevices(this Program self){
		OpenCL.Net.ErrorCode error;
		var numDevicesInfo = OpenCL.Net.Cl.GetProgramInfo(self,ProgramInfo.NumDevices,out error); 
		if (UnityCL.IsError(error)){
			throw new UnityCLException(error);
		}
		var numDevices = numDevicesInfo.CastTo<uint>();
		var devicesInfo = OpenCL.Net.Cl.GetProgramInfo(self,ProgramInfo.Devices,out error);
		if (UnityCL.IsError(error)){
			throw new UnityCLException(error);
		}
		var devices = devicesInfo.CastToArray<Device>(numDevices);
		return devices;
	}
	
}
