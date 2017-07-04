using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RSSReader.Interfaces
{
    public interface ISQLite
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>SQLiteConnection.</returns>
        SQLiteConnection GetConnection();

        /// <summary>
        /// Databases the name.
        /// </summary>
        /// <returns>System.String.</returns>
        string DatabaseName();

        /// <summary>
        /// Databases the path.
        /// </summary>
        /// <returns>System.String.</returns>
        string DatabasePath();

        /// <summary>
        /// Fulls the database path.
        /// </summary>
        /// <returns>System.String.</returns>
        string FullDatabasePath();
    }
}
