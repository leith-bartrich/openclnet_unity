using System;
namespace OpenCL.Net
{
	public static partial class  Cl
	{
		#if UNITY_STANDALONE_OSX
		public const string Library = "/System/Library/Frameworks/OpenCL.framework/Versions/Current/OpenCL";
		#endif

		#if UNITY_IPHONE
		public const string Library = "/System/Library/Frameworks/OpenCL.framework/Versions/Current/OpenCL";
		#endif

	}
}

