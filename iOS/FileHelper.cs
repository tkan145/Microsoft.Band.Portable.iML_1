using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.iML.iOS
{
	public class FileHelper : IFileHelper
	{
		public string GetLocalFilePath(string filename)
		{
			string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string libFolder = Path.Combine(docFolder, "..", "Library");
			var directoryName = Path.Combine(libFolder, "Data");
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}

			return directoryName;
		}

		public async Task SaveTextFile(string fileName, string value)
		{

			var filePath = GetLocalFilePath(fileName);
			//File.WriteAllText(filePath, value);
			using (StreamWriter sw = new StreamWriter(filePath, false))
			{
				await sw.WriteAsync(value);
			}
		}

		public async Task<string> LoadText(string fileName)
		{
			var filePath = GetLocalFilePath(fileName);
			//return await File.ReadAllText(filePath);
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
