using Xamarin.Forms.Controls.GalleryPages.PlatformSpecificsGalleries;

namespace Xamarin.Forms.Controls
{
	public class PlatformSpecificsGallery : ContentPage
	{
		Page _originalRoot;

		public PlatformSpecificsGallery()
		{
			var mdpiOSButton = new Button { Text = "MasterDetailPage (iOS)" };
			var mdpWindowsButton = new Button { Text = "MasterDetailPage (Windows)" };
			var npiOSButton = new Button() { Text = "NavigationPage (iOS)" };
			var npWindowsButton = new Button { Text = "NavigationPage (Windows)" };
			var tbiOSButton = new Button { Text = "TabbedPage (iOS)" };
			var tbWindowsButton = new Button { Text = "TabbedPage (Windows)" };
			var viselemiOSButton = new Button() { Text = "Visual Element (iOS)" };
			var appAndroidButton = new Button() { Text = "Application (Android)" };

			mdpiOSButton.Clicked += (sender, args) => { SetRoot(new MasterDetailPageiOS(new Command(RestoreOriginal))); };
			mdpWindowsButton.Clicked += (sender, args) => { SetRoot(new MasterDetailPageWindows(new Command(RestoreOriginal))); };
			npiOSButton.Clicked += (sender, args) => { SetRoot(NavigationPageiOS.Create(new Command(RestoreOriginal))); };
			npWindowsButton.Clicked += (sender, args) => { SetRoot(new NavigationPageWindows(new Command(RestoreOriginal))); };
			tbiOSButton.Clicked += (sender, args) => { SetRoot(new TabbedPageiOS(new Command(RestoreOriginal))); };
			tbWindowsButton.Clicked += (sender, args) => { SetRoot(new TabbedPageWindows(new Command(RestoreOriginal))); };
			viselemiOSButton.Clicked += (sender, args) => { SetRoot(new VisualElementiOS(new Command(RestoreOriginal))); };
			appAndroidButton.Clicked += (sender, args) => { SetRoot(new ApplicationAndroid(new Command(RestoreOriginal))); };

			Content = new StackLayout
			{
				Children = { mdpiOSButton, mdpWindowsButton, npiOSButton, npWindowsButton, tbiOSButton, tbWindowsButton, viselemiOSButton, appAndroidButton }
			};
		}

		void SetRoot(Page page)
		{
			var app = Application.Current as App;
			if (app == null)
			{
				return;
			}

			_originalRoot = app.MainPage;
			app.SetMainPage(page);
		}

		void RestoreOriginal()
		{
			if (_originalRoot == null)
			{
				return;
			}

			var app = Application.Current as App;
			app?.SetMainPage(_originalRoot);
		}
	}
}