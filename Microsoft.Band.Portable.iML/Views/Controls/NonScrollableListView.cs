using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.Views
{
	public class NonScrollableListView : ListView
	{
		public NonScrollableListView() : base(ListViewCachingStrategy.RecycleElement)
		{
		}
	}
}
