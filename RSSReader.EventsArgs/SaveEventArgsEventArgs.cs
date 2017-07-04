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
    public class SaveEventArgs : EventArgs {
        /// <summary>
        /// Gets or sets the created identifier.
        /// </summary>
        /// <value>The created identifier.</value>
        public int CreatedOrUpdatedId { get; set; }
    }
}
