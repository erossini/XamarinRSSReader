using RSSReader.Data;
using RSSReader.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.Web
{
    /// <summary>
    /// Class RSSClient.
    /// </summary>
    public class RSSClient
    {
        /// <summary>
        /// call URL as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task&lt;List&lt;Post&gt;&gt;.</returns>
        public async Task<List<Post>> CallUrlAsync(string url)
        {
            List<Post> rtn = new List<Post>();
            List<NewsHeader> list = new List<NewsHeader>();
            list.Add(new NewsHeader() { Url = url });

            RssParser rss = new RssParser();
            rtn = await rss.ParseByXMLDocumentAsync(list);

            return rtn;
        }
    }
}
