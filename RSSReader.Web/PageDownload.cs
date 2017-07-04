using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.Web
{
    /// <summary>
    /// Class PageDownload.
    /// </summary>
    public class PageDownload
    {
        /// <summary>
        /// Starts the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> Start(string url)
        {
            string rtn = "";

            try
            {
                rtn = await new HttpClient().GetStringAsync(new Uri(url));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return rtn;
        }
    }
}
