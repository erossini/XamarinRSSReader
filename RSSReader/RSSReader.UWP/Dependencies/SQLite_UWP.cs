using RSSReader.Interfaces;
using RSSReader.UWP.Dependecies;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_UWP))]
namespace RSSReader.UWP.Dependecies
{
    public class SQLite_UWP : ISQLite
    {
        /// <summary>
        /// Databases the name.
        /// </summary>
        /// <returns>The name.</returns>
        public string DatabaseName()
        {
            return "RSSReader.db3";
        }

        /// <summary>
        /// Path this instance.
        /// </summary>
        public string DatabasePath()
        {
            string documentsPath = ApplicationData.Current.LocalFolder.Path; // Documents folder
            return documentsPath;
        }

        /// <summary>
        /// Databases the path.
        /// </summary>
        /// <returns>The path.</returns>
        public string FullDatabasePath()
        {
            // Documents folder
            var path = Path.Combine(DatabasePath(), DatabaseName());
            return path;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            // Create the connection
            var conn = new SQLiteConnection(FullDatabasePath());

            // Return the database connection
            return conn;
        }
    }
}