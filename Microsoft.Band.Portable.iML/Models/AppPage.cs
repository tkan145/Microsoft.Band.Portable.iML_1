

namespace Microsoft.Band.Portable.iML
{
	public class DeepLinkPage
	{
		public AppPage Page { get; set; }
		public string Id { get; set; }
	}
	public enum AppPage
	{
		Dashboard,
		Settings,
	}
}


