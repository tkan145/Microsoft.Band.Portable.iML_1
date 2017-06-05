using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Microsoft.Band.Portable.iML
{
	public interface IFileHelper
	{
		bool Exist(string fileName);
		Task WriteAllText(string fileName, string text);
		Task<string> ReadAllText(string fileName);
		//string GetLocalFilePath(string filename);


		double[,] LoadCsv(string fileName);

	}
}
