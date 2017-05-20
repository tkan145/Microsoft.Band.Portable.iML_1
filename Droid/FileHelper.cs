using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Microsoft.Band.Portable.iML.Droid
{
	public class FileHelper : IFileHelper
	{
		public string GetLocalFilePath(string fileName)
		{
			var documentsPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
			var dirName = Path.Combine(documentsPath, "DataFolder");
			var filePath = Path.Combine(dirName, fileName);
			Directory.CreateDirectory(dirName);
			return filePath;
		}

		public async Task SaveTextFile(string fileName, string value)
		{
			var filePath = GetLocalFilePath(fileName);
			using (StreamWriter sw = new StreamWriter(filePath, false))
			{
				await sw.WriteAsync(value);
			}
		}

		public async Task<string> LoadText(string fileName)
		{
			var filePath = GetLocalFilePath(fileName);
			try
			{   // Open the text file using a stream reader.
				using (StreamReader sr = new StreamReader(filePath))
				{
					// Read the stream to a string, and write the string to the console.
					return await sr.ReadToEndAsync();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
				return null;
			}
		}
	}
}
