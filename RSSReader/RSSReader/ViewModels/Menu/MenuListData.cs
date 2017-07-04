using RSSReader.Helpers;
using RSSReader.Views;
using System;
using System.Collections.Generic;

namespace RSSReader
{
	public class MenuListData : List<MenuItems>
	{
		public MenuListData()
		{
			this.Add(new MenuItems()
			{
				Title = "Feed",
				IconSource = UIHelpers.SetOSImagePath("home.png"),
				TargetType = typeof(PostList)
			});
        }
	}
}