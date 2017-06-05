﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Microsoft.Band.Portable.iML.iOS
{
	public class FileHelper : IFileHelper
	{
		public bool Exist(string fileName)
		{
			string filePath = GetLocalFilePath(fileName);
			return File.Exists(filePath);
		}

		public string GetLocalFilePath(string fileName)
		{
			string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			string libFolder = Path.Combine(docFolder, "..", "Library");
			var directoryName = Path.Combine(libFolder, "Data");
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			return Path.Combine(directoryName, fileName);
		}

		public async Task WriteAllText(string fileName, string value)
		{
			var filePath = GetLocalFilePath(fileName);
			//File.WriteAllText(filePath, value);
			using (StreamWriter sw = new StreamWriter(filePath, false))
			{
				await sw.WriteAsync(value);
			}
		}

		public async Task<string> ReadAllText(string fileName)
		{
			var filePath = GetLocalFilePath(fileName);
			//return await File.ReadAllText(filePath);
			try
			{
				using (StreamReader sr = new StreamReader(filePath))
				{
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

		public double[,] LoadCsv(string fileName)
		{
			string files;
			var filePath = GetLocalFilePath(fileName);
			if (File.Exists(filePath))
			{
				try
				{   // Open the text file using a stream reader.
					using (StreamReader sr = new StreamReader(filePath))
					{
						// Read the stream to a string, and write the string to the console.
						files = sr.ReadToEnd();
						// Split into lines.
						files = files.Replace('\n', '\r');
						string[] lines = files.Split(new char[] { '\r' },
							StringSplitOptions.RemoveEmptyEntries);
						// See how many rows and columns there are.
						int num_rows = lines.Length;
						int num_cols = lines[0].Split(',').Length;
						// Allocate the data array.
						double[,] values = new double[num_rows, num_cols];

						// Load the array.
						for (int r = 0; r < num_rows; r++)
						{
							string[] line_r = lines[r].Split(',');
							for (int c = 0; c < num_cols; c++)
							{
								values[r, c] = double.Parse(line_r[c]);
							}
						}
						return values;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("The file could not be read:");
					Console.WriteLine(e.Message);
					return null;
				}
			}
			else
				return null;
		}


	}
}
