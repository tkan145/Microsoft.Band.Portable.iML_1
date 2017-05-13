using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace Microsoft.Band.Portable.iML
{
	public class SensorViewModel : BaseViewModel
	{
		public SensorViewModel(INavigation navigation)
					: base(navigation)
		{
			AllCategory = new SensorCategory
			{
				Name = "All",
				IsEnabled = true,
				IsFiltered = Settings.ShowAllCategories
			};
			AllCategory.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "IsFiltered")
					SetShowAllCategories(AllCategory.IsFiltered);
			};

		}

		public SensorCategory AllCategory { get; }
		public List<SensorCategory> Categories { get; } = new List<SensorCategory>();
		private void SetShowAllCategories(bool showAll)
		{
			Settings.ShowAllCategories = showAll;
			foreach (var category in Categories)
			{
				category.IsEnabled = !Settings.ShowAllCategories;
				category.IsFiltered = Settings.ShowAllCategories || Settings.FilteredCategories.Contains(category.Name);
			}
		}

		public async Task LoadCategoriesAsync()
		{
			Categories.Clear();
			var items = await StoreManager.CategoryStore.GetItemsAsync();
			try
			{
				if (!items.Any())
					items = await StoreManager.CategoryStore.GetItemsAsync(true);
			}
			catch
			{
				items = await StoreManager.CategoryStore.GetItemsAsync(true);
			}

			foreach (var category in items.OrderBy(c => c.Name))
			{
				category.IsFiltered = Settings.ShowAllCategories || Settings.FilteredCategories.Contains(category.Name);
				category.IsEnabled = !Settings.ShowAllCategories;
				Categories.Add(category);
			}

			Save();
		}


		public void Save()
		{
			Settings.FilteredCategories = string.Join("|", Categories?.Where(c => c.IsFiltered).Select(c => c.Name));
		}
	}
}
