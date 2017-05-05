﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FormsToolkit.Droid;

namespace Microsoft.Band.Portable.iML.Droid
{
	[Activity(Label = "Microsoft.Band.Portable.iML.Droid",
			  Icon = "@drawable/icon",
			  Theme = "@style/MyTheme",
			  MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			Toolkit.Init();
			LoadApplication(new App());
		}
	}
}
