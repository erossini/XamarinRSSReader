using System;
using System.Collections.Generic;
using RSSReader.Data;

namespace RSSReader.EventsArgs{
    /// <summary>
    /// Class SaveSourceEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SaveSourceEventArgs : EventArgs {
        public SaveSourceEventArgs() {
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public Source SourceDetails { get; set; }

        /// <summary>
        /// Gets or sets the save on database.
        /// </summary>
        /// <value>The save on database.</value>
        public bool SaveOnDatabase { get; set; }
    }
}
