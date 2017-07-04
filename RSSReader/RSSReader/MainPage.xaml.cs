using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RSSReader
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Web.RSSClient client = new Web.RSSClient();
            client.CallUrlAsync("http://feeds.bbci.co.uk/news/uk/rss.xml");
            client.CallUrlAsync("http://feeds.bbci.co.uk/news/technology/rss.xml");
            client.CallUrlAsync("http://feeds.reuters.com/reuters/technologyNews?format=xml");
            client.CallUrlAsync("http://feeds.reuters.com/reuters/UKdomesticNews?format=xml");
        }
    }
}
