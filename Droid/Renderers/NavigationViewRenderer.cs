using System;
using Xamarin.Forms.Platform.Android;
using Android.Support.Design.Widget;
using Android.Runtime;
using Xamarin.Forms;
using Microsoft.Band.Portable.iML.Droid;
using Android.Widget;
using Android.Views;

[assembly: ExportRenderer(typeof(Microsoft.Band.Portable.iML.Views.NavigationView), typeof(NavigationViewRenderer))]
namespace Microsoft.Band.Portable.iML.Droid
{
	public class NavigationViewRenderer : ViewRenderer<Microsoft.Band.Portable.iML.Views.NavigationView, NavigationView>
	{
		NavigationView navView;

		protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Band.Portable.iML.Views.NavigationView> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement != null || Element == null)
				return;

			var view = Inflate(Forms.Context, Resource.Layout.nav_view, null);
			navView = view.JavaCast<NavigationView>();

			navView.NavigationItemSelected += NavView_NavigationItemSelected;
			SetNativeControl(navView);

			var header = navView.GetHeaderView(0);
			navView.SetCheckedItem(Resource.Id.nav_feed);
		}

		public override void OnViewRemoved(Android.Views.View child)
		{
			base.OnViewRemoved(child);
			navView.NavigationItemSelected -= NavView_NavigationItemSelected;
		}

		IMenuItem previousItem;

		void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{


			if (previousItem != null)
				previousItem.SetChecked(false);

			navView.SetCheckedItem(e.MenuItem.ItemId);

			previousItem = e.MenuItem;

			int id = 0;
			switch (e.MenuItem.ItemId)
			{
				case Resource.Id.nav_feed:
                    id = (int)AppPage.Dashboard;
					break;
				
				case Resource.Id.nav_settings:
					id = (int)AppPage.Settings;
					break;
			}
			this.Element.OnNavigationItemSelected(new Microsoft.Band.Portable.iML.Views.NavigationItemSelectedEventArgs
			{

				Index = id
			});
		}

	}
}
