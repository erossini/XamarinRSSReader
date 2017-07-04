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
    public class ParamErrorEventArgs : EventArgs {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}
