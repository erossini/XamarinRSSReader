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
    public class FormErrorEventArgs : EventArgs {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormErrorEventArgs"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="message">The message.</param>
        public FormErrorEventArgs(string field, string message) {
            this.Field = field;
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>The field.</value>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}
