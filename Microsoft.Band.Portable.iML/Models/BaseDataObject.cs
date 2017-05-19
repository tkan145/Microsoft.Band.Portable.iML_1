using System;
using GalaSoft.MvvmLight;
using SQLite;
namespace Microsoft.Band.Portable.iML
{
	public interface IBaseDataObject
	{
		string Id { get; set; }
	}

	public class BaseDataObject : ObservableObject, IBaseDataObject
	{
		public BaseDataObject()
		{
			Id = Guid.NewGuid().ToString();
		}

		//[Newtonsoft.Json.JsonProperty("Id")]
		[PrimaryKey]
		public string Id { get; set; }

	}
}
