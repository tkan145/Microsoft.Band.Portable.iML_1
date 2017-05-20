using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Microsoft.Band.Portable.iML
{
	public interface IFileHelper
	{
		string GetLocalFilePath(string filename);
		Task SaveTextFile(string fileName, string text);
		Task<string> LoadText(string fileName);
	}
}
