using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using Xamarin.Forms;
using RSSReader.iOS.Dependencies;
using RSSReader.Interfaces;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace RSSReader.iOS.Dependencies {
    /// <summary>
    /// Class SQLite_iOS.
    /// </summary>
    /// <seealso cref="WordBankEasy.Data.Interfaces.ISQLite" />
    public class SQLite_iOS : ISQLite {
        /// <summary>
        /// Databases the name.
        /// </summary>
        /// <returns>The name.</returns>
        public string DatabaseName()
        {
            return "WordBankEasy.db3";
        }

        /// <summary>
        /// Path this instance.
        /// </summary>
        public string DatabasePath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
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