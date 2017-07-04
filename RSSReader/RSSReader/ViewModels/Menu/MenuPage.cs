using System;
using Plugin.VersionTracking;
using Xamarin.Forms;
using RSSReader.Helpers;

namespace RSSReader
{
	public partial class MenuPage : ContentPage
	{
		public ListView Menu { get; set; }
		public ListView MenuUtilities { get; set; }

		public MenuPage()
		{
			Title = "menu"; // The Title property must be set.
			BackgroundColor = Color.FromHex("#FF6600");
			if (Device.RuntimePlatform == Device.iOS)
			{
				Icon = UIHelpers.SetOSImagePath("settings.png");
			}

			Menu = new MenuListView(new MenuListData());

			var menuLabel = new ContentView
			{
				Padding = new Thickness(10, 36, 0, 5),
				Content = new Image
				{
					Source = UIHelpers.SetOSImagePath("rssreader_logo.png"),
					WidthRequest = 150
				}
			};

			var layout = new StackLayout
			{
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			// app version
			var lbl = new Label
			{
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Text = $"Version: {CrossVersionTracking.Current.CurrentVersion}",
				HorizontalTextAlignment = TextAlignment.Center,
			};
			layout.Children.Add(menuLabel);
			layout.Children.Add(Menu);
			layout.Children.Add(lbl);

			Content = layout;
		}
	}
}