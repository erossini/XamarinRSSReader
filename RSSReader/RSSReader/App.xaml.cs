using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.VersionTracking;
using RSSReader.Models.Messages;
using RSSReader.Repository;
using Xamarin.Forms;
//using Microsoft.Azure.Mobile;
//using Microsoft.Azure.Mobile.Analytics;
//using Microsoft.Azure.Mobile.Crashes;

namespace RSSReader
{
    public partial class App : Application
    {
        Logs.MobileCenter log = new Logs.MobileCenter();

        public App()
        {
            InitializeComponent();

            MainPage = new RootPage();
        }

        /// <summary>
        /// Gets the main page.
        /// </summary>
        /// <returns>The main page.</returns>
        public static Page GetMainPage()
        {
            return new RootPage();
        }

        protected override void OnStart()
        {
            // https://mobile.azure.com/users/HenryUK/apps/RSSReader
            //MobileCenter.LogLevel = LogLevel.Verbose;
            //MobileCenter.Start("ios=82acb881-fac1-4002-b76b-d067cb93ac2f;" +
            //                   "android=d7183102-390c-4483-a02a-99ef79ef0b66;" +
            //                   "uwp=940beb73-cd57-4b16-82d3-572850b604ff",
            //                   typeof(Analytics), typeof(Crashes));

            // send first message to Azure
			log.SendEvent("OnStart", "App", CrossVersionTracking.Current.CurrentVersion);

			// check if the last session the app was in crash
			//bool didAppCrash = Crashes.HasCrashedInLastSession;
			//if (didAppCrash)
			//{
			//	MessagingCenter.Send(new SorryMessage(), "SorryMessage");
			//}

			// db initialize
			RSSDatabase db = new RSSDatabase();
			db.InitDatabase();
			db = null;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            log.SendEvent("OnSleep", "App", CrossVersionTracking.Current.CurrentVersion);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            log.SendEvent("OnResume", "App", CrossVersionTracking.Current.CurrentVersion);
        }
    }
}
