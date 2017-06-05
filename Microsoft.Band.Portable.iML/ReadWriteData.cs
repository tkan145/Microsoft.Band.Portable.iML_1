using System;
//using System.IO;
using System.Threading.Tasks;
using System.Text;
using PCLStorage;
namespace Microsoft.Band.Portable.iML
{
	public class ReadWriteData
	{
		private const string FILE_NAME = "test.csv";
		public ReadWriteData()
		{

		}

		public async Task<IFolder> NavigateToFolder(string targetFolder)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync(targetFolder, CreationCollisionOption.OpenIfExists);
			return folder;
		}

		public static async Task WriteFileAsync(string fileName, string[] text)
		{
			//try
			//{
			//	var file = await FileSystem.Current.LocalStorage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

			//	using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
			//	{
			//		using (var writer = new StreamWriter(stream))
			//		{
			//			await writer.WriteLineAsync("\r\nLog Entry : ");
			//			await writer.WriteLineAsync("{0}", DateTime.Now.ToString());
			//			foreach (string line in text)
			//			{
			//				await writer.WriteLineAsync(line);
			//			}
			//			await writer.WriteAsync("-------------------------------");
			//		}
			//	}
			//}
			//catch (Exception ex)
			//{
			//	await DisplayAlert("Error message: ", ex.Message, "OK");
			//}

			//await DisplayAlert("Noerrors: ", "Is  file there?", "OK");

			//ExistenceCheckResult exist = await rootFolder.CheckExistsAsync("lou.txt");
			//if (exist == ExistenceCheckResult.FileExists)
			//{
			//	await DisplayAlert("File Exists?", "Yes, file does exist", "OK");
			//}
			//else
			//	await DisplayAlert("File Exists?", "No, file does NOT exist", "OK");
		}




		public async Task ReadFileAsync(string fileName, string localFolder)
		{
			StringBuilder contents = new StringBuilder();
			string nextLine;
			int lineCounter = 1;

			var folder = await NavigateToFolder(localFolder);
			if (await folder.CheckExistsAsync(fileName) == ExistenceCheckResult.NotFound)
			{
				// return empty
			}
			var file = await folder.GetFileAsync(fileName);

			using (var stream = await file.OpenAsync(FileAccess.Read))
			{
				//using (var reader = new StreamReader(stream))
				//{
				//	while ((nextLine = await reader.ReadLineAsync()) != null)
				//	{
				//		contents.AppendFormat("{0}. ", lineCounter);
				//		contents.Append(nextLine);
				//		contents.AppendLine();
				//		lineCounter++;
				//	}
				//}
			}

			//var jsonCompanies = await file.ReadAllTextAsync();

			//if (string.IsNullOrEmpty(jsonCompanies)) return new List<Models.Company>();

			//var companies = JsonConvert.DeserializeObject<IEnumerable<Models.Company>>(jsonCompanies);

			//return companies;
		}

		public async Task WriteImageAsync(IFolder folder, string[] images)
		{
			foreach (var image in images)
			{
				var file = await folder.CreateFileAsync(image + ".png", CreationCollisionOption.ReplaceExisting);
				using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
				{
					//byte[] imageBuffer = await 
					//await stream.WriteAsync(imageBuffer, 0, imageBuffer.Length);
					//image.ImageUri = file.Path;
				}
			}
		}
	}
}
