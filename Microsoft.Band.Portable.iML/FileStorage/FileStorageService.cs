using System;

using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using PCLStorage;

namespace Microsoft.Band.Portable.iML
{
	public class FileStorageService
	{
		public async Task SaveFile<T>(string key, T value)
		{
			XDocument doc = new XDocument();
			using (var writer = doc.CreateWriter())
			{
				var serializer = new XmlSerializer(typeof(T));
				serializer.Serialize(writer, value);
			}

			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync("Cache",
				CreationCollisionOption.OpenIfExists);
			IFile file = await folder.CreateFileAsync(key + ".txt",
				CreationCollisionOption.ReplaceExisting);

			await file.WriteAllTextAsync(doc.ToString());
		}

		public async Task<T> GetFile<T>(string key)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync("Cache",
				CreationCollisionOption.OpenIfExists);

			ExistenceCheckResult isFileExisting = await folder.CheckExistsAsync(key + ".txt");

			if (!isFileExisting.ToString().Equals("NotFound"))
			{
				//try
				//{
				//	IFile file = await folder.CreateFileAsync(key + ".txt",
				//	CreationCollisionOption.OpenIfExists);

				//	String languageString = await file.ReadAllTextAsync();

				//	XmlSerializer oXmlSerializer = new XmlSerializer(typeof(T));
				//	return (T)oXmlSerializer.Deserialize(new StringReader(languageString));
				//}
				//catch (Exception ex)
				//{
				//	return default(T);
				//}
			}

			return default(T);
		}

		public async Task DeleteFile(string key, string fileExtension)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync("Cache",
				CreationCollisionOption.OpenIfExists);
			ExistenceCheckResult isFileExisting = await folder.CheckExistsAsync(key + fileExtension);
			if (!isFileExisting.ToString().Equals("NotFound"))
			{
				try
				{
					IFile file = await folder.CreateFileAsync(key + ".txt",
					CreationCollisionOption.OpenIfExists);

					await file.DeleteAsync();
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		private async Task<IFolder> NavigateToFolder(string targetFolder)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync(targetFolder, CreationCollisionOption.OpenIfExists);
			return folder;
		}


		private async Task<T> ReadTextFromFile<T>(string fileName)
		{
			var folder = await NavigateToFolder("folder");
			if (!(await folder.CheckExistsAsync(fileName) == ExistenceCheckResult.NotFound))
			{
				try
				{
					IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

					string data = await file.ReadAllTextAsync();

				}
				catch (Exception ex)
				{
					return default(T);
					throw new Exception(ex.Message);
				}
			}
			return default(T);
		}

		private async Task StoreImages(IFolder folder, string imageName, byte[] imageBuffer)
		{
			var file = await folder.CreateFileAsync(imageName + ".jpg", CreationCollisionOption.ReplaceExisting);
			using (var fileHandler = await file.OpenAsync(FileAccess.ReadAndWrite))
			{
				await fileHandler.WriteAsync(imageBuffer, 0, imageBuffer.Length);
			}
		}
	}
}
