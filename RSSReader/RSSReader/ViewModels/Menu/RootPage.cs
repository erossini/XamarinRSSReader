using System;
using RSSReader.Helpers;
using Xamarin.Forms;
using RSSReader.Views;

namespace RSSReader
{
    public class RootPage : MasterDetailPage
    {
        MenuPage menuPage;

        public RootPage()
        {
            menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItems);

            Master = menuPage;

            Detail = new NavigationPage(new PostList())
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.FromHex("#FF6600")
            };

        }

        void NavigateTo(MenuItems menu)
        {
            if (menu == null)
                return;

            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);
            Detail = new NavigationPage(displayPage)
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.FromHex("#FF6600")
            };

            menuPage.Menu.SelectedItem = null;
            menuPage.MenuUtilities.SelectedItem = null;
            IsPresented = false;
        }
    }
}