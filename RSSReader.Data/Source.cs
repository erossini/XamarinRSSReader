using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.Data
{
    /// <summary>
    /// Class Source.
    /// </summary>
    /// <seealso cref="RSSReader.BaseTableRSSReader" />
    [Table("Source")]
    public class Source : BaseTableRSSReader
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the source URL.
        /// </summary>
        /// <value>The source URL.</value>
        public string SourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>The image path.</value>
        [Ignore]
        public string ImagePath { get; set; }
    }
}
