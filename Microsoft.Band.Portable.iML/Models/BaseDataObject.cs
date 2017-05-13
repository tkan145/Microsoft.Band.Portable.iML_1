using System;
using GalaSoft.MvvmLight;

namespace Microsoft.Band.Portable.iML
{
	public interface IBaseDataObject
	{
		int ID { get; set; }
	}

	public class BaseDataObject : ObservableObject, IBaseDataObject
	{
		public BaseDataObject()
		{
			//ID = Guid.NewGuid().ToString();
		}

		public string RemoteId { get; set; }

		//[Newtonsoft.Json.JsonProperty("Id")]
		public int ID { get; set; }

	}
}
