using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.EventsArgs {
    /// <summary>
    /// FormErrorEventArgs
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class FormSaveErrorEventArgs : EventArgs {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormErrorEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public FormSaveErrorEventArgs(string message) {
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}
