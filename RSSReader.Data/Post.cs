using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace RSSReader.Data {
    /// <summary>
    /// Class for post table.
    /// </summary>
    /// <seealso cref="RSSReader.Data.BaseTable" />
    [Table("Post")]
    public class Post : BaseTableRSSReader {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title identifier.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description identifier.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the news source.
        /// </summary>
        /// <value>
        /// The news source identifier.
        /// </value>
        public string NewsSource { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link identifier.
        /// </value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the image url.
        /// </summary>
        /// <value>
        /// The image url identifier.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        /// <value>
        /// The publication date.
        /// </value>
        public DateTime PubDate { get; set; }

        /// <summary>
        /// Gets or sets the HTML post.
        /// </summary>
        /// <value>The HTML post.</value>
        public string HtmlPost { get; set; }

        [Ignore]
        public Color CircleColor { get; set; }

    }
}
