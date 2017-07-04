using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.EventsArgs {
    /// <summary>
    ///  SaveEventArgs
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class LoadingEventArgs : EventArgs {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value><c>true</c> if this instance is loading; otherwise, <c>false</c>.</value>
        public bool IsLoading { get; set; } = false;
    }
}
