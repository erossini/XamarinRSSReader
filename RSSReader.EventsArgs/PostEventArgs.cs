using System;
using System.Collections.Generic;
using RSSReader.Data;

namespace RSSReader.EventsArgs{
    /// <summary>
    /// Class SavePostEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SavePostEventArgs : EventArgs {
        public SavePostEventArgs() {
        }

        /// <summary>
        /// Gets or sets the post.
        /// </summary>
        /// <value>The post.</value>
        public Post PostDetails { get; set; }

        /// <summary>
        /// Gets or sets the save on database.
        /// </summary>
        /// <value>The save on database.</value>
        public bool SaveOnDatabase { get; set; }
    }
}
