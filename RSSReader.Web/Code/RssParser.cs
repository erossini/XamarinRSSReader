using RSSReader.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
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

                    try
                    {
                        HttpResponseMessage response = await webClient.GetAsync(xmlUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            XDocument document = XDocument.Parse(result);

                            try
                            {
                                var tmp = from u in document.Descendants("item") select u;
                                foreach(var itm in tmp)
                                {
                                    if (itm != null)
                                    {
                                        Post post = new Post();
                                        post.Category = itm.Element("category")?.Value;
                                        post.NewsSource = string.IsNullOrEmpty(item.NewsSource) ? "" : item.NewsSource;
                                        post.Title = itm.Element("title")?.Value;
                                        post.Description = ReduceString(itm.Element("description")?.Value);
                                        post.Link = itm.Element("link")?.Value;
                                        post.ImageUrl = GetImageUrl(itm);
                                        post.PubDate = DateTime.Parse(itm.Element("pubDate").Value);
                                        newslist.Add(post);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                log.SendEvent("RssParser error", "", "", exception: ex);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        log.SendEvent("RssParser error", "", "", exception: ex);
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

        public string ReduceString(string content)
        {
            content = StripHTML(content);
            if (content.Length > 200)
                content = content.Substring(0, 200).Trim() + "...";
            return content;
        }

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
