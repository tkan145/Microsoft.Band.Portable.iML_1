using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using FormsToolkit;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.Views
{
	public class RootPageAndroid : MasterDetailPage
	{
		Dictionary<int, iMLNavigationPage> pages;


		//bool isRunning = false;
		public RootPageAndroid()
		{
			pages = new Dictionary<int, iMLNavigationPage>();
			Master = new MenuPage(this);

			pages.Add(0, new iMLNavigationPage(new DashboardPage()));

			Detail = pages[0];
		}
		public async Task NavigateAsync(int menuId)
		{
			iMLNavigationPage newPage = null;
			if (!pages.ContainsKey(menuId))
			{
				//Debug.WriteLine("menuID {0}", menuId);
				//only cache specific pages
				switch (menuId)
				{
					case (int)AppPage.Dashboard: //Feed
						pages.Add(menuId, new iMLNavigationPage(new DashboardPage()));
						break;
					case (int)AppPage.Settings://Settings
						newPage = new iMLNavigationPage(new SettingsPage());
						break;
				}
			}

			if (newPage == null)
				newPage = pages[menuId];

			if (newPage == null)
				return;

			//if we are on the same tab and pressed it again.
			if (Detail == newPage)
			{
				await newPage.Navigation.PopToRootAsync();
			}

			Detail = newPage;
		}
	}
}
