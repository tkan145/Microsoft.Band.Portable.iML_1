using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class CategoryCell : ViewCell
	{
		public CategoryCell()
		{
			if (Device.RuntimePlatform == Device.Windows || Device.RuntimePlatform == Device.WinPhone)
				Height = 50;
			else
				Height = 44;
			View = new CategoryCellView();
		}
	}
	public partial class CategoryCellView : ContentView
	{
		public CategoryCellView()
		{
			InitializeComponent();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			var category = BindingContext as SensorCategory;
			if (category == null)
				return;
			if (string.IsNullOrWhiteSpace(category.Color))
			{
				Grid.SetColumn(LabelName, 0);
				Grid.SetColumnSpan(LabelName, 2);
			}
		}
	}
}
