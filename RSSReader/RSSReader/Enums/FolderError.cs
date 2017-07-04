using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.Enums
{
    /// <summary>
    /// Enum FolderError
    /// </summary>
    public enum FolderError
    {
        /// <summary>
        /// The none
        /// </summary>
        None,

        /// <summary>
        /// The permission error
        /// </summary>
        PermissionErrorOnDirectories,

        /// <summary>
        /// The permission error on files
        /// </summary>
        PermissionErrorOnFileCreate,

        /// <summary>
        /// The permission error on file read
        /// </summary>
        PermissionErrorOnFileRead,
    }
}
