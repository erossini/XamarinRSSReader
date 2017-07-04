using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RSSReader.Helpers
{
    public static class UIHelpers
    {
        /// <summary>
        /// Sets the os image path.
        /// </summary>
        /// <param name="ImageName">Name of the image.</param>
        /// <returns>System.String.</returns>
        public static string SetOSImagePath(string ImageName)
        {
            string rtn = ImageName;

            if (Device.RuntimePlatform == Device.Windows || Device.RuntimePlatform == Device.WinPhone)
            {
                rtn = "Images/" + ImageName;
            }

            return rtn;
        }
    }
}
