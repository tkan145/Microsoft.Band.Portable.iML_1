// Helpers/Settings.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Microsoft.Band.Portable.iML
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public class Settings : INotifyPropertyChanged
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}
		static Settings settings;


		// <summary>
		/// Gets or sets the current settings. This should always be used
		/// </summary>
		/// <value>The current.</value>
		public static Settings Current
		{
			get
			{
				return settings ?? (settings = new Settings());
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

		#endregion


		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
			}
		}

		const string FirstRunKey = "first_run";
		static readonly bool FirstRunDefault = true;

		/// <summary>
		/// Gets or sets a value indicating whether the user wants to see favorites only.
		/// </summary>
		/// <value><c>true</c> if favorites only; otherwise, <c>false</c>.</value>
		public bool FirstRun
		{
			get { return AppSettings.GetValueOrDefault<bool>(FirstRunKey, FirstRunDefault); }
			set
			{
				if (AppSettings.AddOrUpdateValue<bool>(FirstRunKey, value))
					OnPropertyChanged();
			}

		}

		bool isConnected;
		public bool IsConnected
		{
			get { return isConnected; }
			set
			{
				if (isConnected == value)
					return;
				isConnected = value;
				OnPropertyChanged();
			}
		}

		const string FilteredCategoriesKey = "filtered_categories";
		static readonly string FilteredCategoriesDefault = string.Empty;


		public string FilteredCategories
		{
			get { return AppSettings.GetValueOrDefault<string>(FilteredCategoriesKey, FilteredCategoriesDefault); }
			set
			{
				if (AppSettings.AddOrUpdateValue<string>(FilteredCategoriesKey, value))
					OnPropertyChanged();
			}
		}

		const string ShowAllCategoriesKey = "all_categories";
		static readonly bool ShowAllCategoriesDefault = true;

		/// <summary>
		/// Gets or sets a value indicating whether the user wants to show all categories.
		/// </summary>
		/// <value><c>true</c> if show all categories; otherwise, <c>false</c>.</value>
		public bool ShowAllCategories
		{
			get { return AppSettings.GetValueOrDefault<bool>(ShowAllCategoriesKey, ShowAllCategoriesDefault); }
			set
			{
				if (AppSettings.AddOrUpdateValue<bool>(ShowAllCategoriesKey, value))
					OnPropertyChanged();
			}
		}

		public bool HasFilters => (!string.IsNullOrWhiteSpace(FilteredCategories) && !ShowAllCategories);

		const string DatabaseIdKey = "offline_database";
		static readonly int DatabaseIdDefault = 0;
		public static int DatabaseId
		{
			get { return AppSettings.GetValueOrDefault<int>(DatabaseIdKey, DatabaseIdDefault); }
			set
			{
				AppSettings.AddOrUpdateValue<int>(DatabaseIdKey, value);
			}
		}

		public static int UpdateDataBaseId()
		{
			return DatabaseId++;
		}



		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName]string name = "") =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		#endregion
	}
}