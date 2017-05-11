using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;

namespace Microsoft.Band.Portable.iML
{
	public class FileService : IFileService
	{
		private IEnumerable<Model> _models;
		private readonly HttpClient _httpClient;

		private const string ModelsFolder = "Models";
		private const string ModelsFileName = "models.json";
		public FileService()
		{
		}

		#region Write/Update Models
		public async Task UpdateItemsAsync()
		{
			// URL should point to where your service is running
			const string uri = "http://offlinestorageserver.azurewebsites.net/api/values";
			var httpResult = await _httpClient.GetAsync(uri);
			var jsonCompanies = await httpResult.Content.ReadAsStringAsync();

			var models = JsonConvert.DeserializeObject<ICollection<Model>>(jsonCompanies);

			var folder = await NavigateToFolder(ModelsFolder);
			await StoreImagesLocallyAndUpdatePath(folder, models);
			await SerializeModels(folder, models);

			_models = models;
		}

		public async static Task SaveImage(this byte[] image, IFolder folder, string imageName)
		{
			var file = await folder.CreateFileAsync(imageName + ".jpg", CreationCollisionOption.ReplaceExisting);
			using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
			{
				await stream.WriteAsync(image, 0, image.Length);
			}
		}

		public async static Task<byte[]> LoadImage(this byte[] image, IFolder folder, string imageName)
		{
			var file = await folder.GetFileAsync(imageName);
			using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
			{
				long length = stream.Length;
				byte[] streamBuffer = new byte[length];
				stream.Read(streamBuffer, 0, (int)length);
				return streamBuffer;
			}
		}

		private async Task StoreImagesLocallyAndUpdatePath(IFolder folder, IEnumerable<Model> companies)
		{
			foreach (var company in companies)
			{
				var file = await folder.CreateFileAsync(company.Name + ".jpg", CreationCollisionOption.ReplaceExisting);
				using (var fileHandler = await file.OpenAsync(FileAccess.ReadAndWrite))
				{
					var httpResponse = await _httpClient.GetAsync(company.ImageUri);
					byte[] imageBuffer = await httpResponse.Content.ReadAsByteArrayAsync();
					await fileHandler.WriteAsync(imageBuffer, 0, imageBuffer.Length);

					//company.ImageUri = file.Path;
				}
			}
		}

		private async Task<bool> SerializeModels(IFolder folder, ICollection<Model> models)
		{
			IFile file = await folder.CreateFileAsync(ModelsFileName, CreationCollisionOption.ReplaceExisting);
			var modelsString = JsonConvert.SerializeObject(models);
			await file.WriteAllTextAsync(modelsString);

		}
		#endregion
		#region GetModels
		public async Task<IEnumerable<Model>> GetItemsAsync()
		{
			return _models ?? (_models = await ReadFromFile());
		}

		public async Task<IEnumerable<Model>> ReadFromFile()
		{
			var folder = await NavigateToFolder(ModelsFolder);

			if ((await folder.CheckExistsAsync(ModelsFileName)) == ExistenceCheckResult.NotFound)
			{
				return new List<Model>();
			}

			IFile file = await folder.GetFileAsync(ModelsFileName);
			var json = await file.ReadAllTextAsync();

			if (string.IsNullOrEmpty(json)) return new List<Model>();

			return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Model>>(json));
		}

		public async Task<bool> DeleteFile(this string fileName, IFolder folder = null)
		{
			if ((await folder.CheckExistsAsync(ModelsFileName)) == ExistenceCheckResult.FileExists)
			{
				IFile file = await folder.GetFileAsync(fileName);
				await file.DeleteAsync();
				return true;
			}
			return false;

		}
		#endregion


		private static async Task<IFolder> NavigateToFolder(string targetFolder)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync(targetFolder,
				CreationCollisionOption.OpenIfExists);

			return folder;
		}
	}
}
