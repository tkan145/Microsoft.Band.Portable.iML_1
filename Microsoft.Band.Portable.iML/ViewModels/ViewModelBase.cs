using System;
using MvvmHelpers;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class ViewModelBase : BaseViewModel
	{
		protected INavigation Navigation { get; }
		public ViewModelBase(INavigation navigation = null)
		{
			Navigation = navigation;
		}
	}
}
