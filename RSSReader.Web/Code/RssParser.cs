using RSSReader.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RSSReader.Web.Code
{
    public class RssParser
    {
        Logs.MobileCenter log = new Logs.MobileCenter();
        List<Post> newslist;
        Uri xmlUrl;

        // Parse the xml using XMLDocument class.
        public async Task<List<Post>> ParseByXMLDocumentAsync(List<NewsHeader> source)
        {
            int i = 0;
            newslist = new List<Post>();

            foreach (NewsHeader item in source)
            {
                using (var webClient = new HttpClient())
                {
                    xmlUrl = new Uri(item.Url);
                    HttpResponseMessage response = await webClient.GetAsync(xmlUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        XDocument document = XDocument.Parse(result);

                        try
                        {
                            var temp = ((from u in document.Descendants("item")
                                         select new Post()
                                         {
                                             Id = i++,
                                             Category = u.Element("category")?.Value,
                                             NewsSource = item.NewsSource,
                                             Header = item.Header,
                                             Title = u.Element("title")?.Value,
                                             Description = u.Element("description")?.Value,
                                             Link = new Uri(u.Element("link").Value).ToString(),
                                             ImageUrl = GetImageUrl(u),
                                             PubDate = DateTime.Parse(u.Element("pubDate").Value)
                                         }).ToList());
                            newslist.AddRange(temp);
                        }
                        catch(Exception ex)
                        {
                            log.SendEvent("RssParser error", "", "", exception: ex);
                        }
                    }
                }
            }

            return newslist;
        }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.String.</returns>
        public string GetImageUrl(XElement item)
        {
            string rtn = "";
            XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");

            try
            {
                if (item.Element(media + "thumbnail") != null)
                {
                    rtn = item.Element(media + "thumbnail").Attribute("url").Value;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return rtn;
        }
    }
}
