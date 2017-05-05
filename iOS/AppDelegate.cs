using System;
using System.Collections.Generic;
using System.Linq;
using FormsToolkit.iOS;
using Foundation;
using Refractored.XamForms.PullToRefresh.iOS;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using UIKit;

namespace Microsoft.Band.Portable.iML.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			new SfChartRenderer();
			PullToRefreshLayoutRenderer.Init();
			Toolkit.Init();
			ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
