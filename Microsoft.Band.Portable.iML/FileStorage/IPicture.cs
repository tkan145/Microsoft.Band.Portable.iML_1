using System;
namespace Microsoft.Band.Portable.iML
{
	public interface IPicture
	{
		void SaveImageToDisk(string imageName, byte[] data);
		string GetImageFromDisk(string imageName);
	}
}
