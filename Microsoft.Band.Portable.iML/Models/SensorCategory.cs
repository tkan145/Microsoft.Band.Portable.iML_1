using System;
namespace Microsoft.Band.Portable.iML
{
	public class SensorCategory : BaseDataObject
	{
		// <summary>
		/// Gets or sets the name that is displayed during filtering
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }


		/// <summary>
		/// Gets or sets the short name/code that is displayed on the sessions page.
		/// For instance the short name for Xamarin.Forms is X.Forms
		/// </summary>
		/// <value>The short name.</value>
		public string ShortName { get; set; }

		/// <summary>
		/// Gets or sets the color in Hex form, for instance #FFFFFF
		/// </summary>
		/// <value>The color.</value>
		public string Color { get; set; }


		bool filtered;
		//[JsonIgnore]
		public bool IsFiltered
		{
			get { return filtered; }
			set
			{
				Set(ref filtered, value);
			}
		}


		bool enabled;
		//[JsonIgnore]
		public bool IsEnabled
		{
			get { return enabled; }
			set { Set(ref enabled, value); }
		}

	}
}
