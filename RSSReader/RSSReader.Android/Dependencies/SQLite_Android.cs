using RSSReader.Droid.Dependencies;
using RSSReader.Droid.Helpers;
using RSSReader.Enums;
using RSSReader.Interfaces;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace RSSReader.Droid.Dependencies {
    public class SQLite_Android : ISQLite {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLite_Android"/> class.
        /// </summary>
        public SQLite_Android() { }

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
            FolderError error;
            string documentsPath = new FolderUtilities().GetAccessibleFolderFromUser(out error); // Documents folder
            documentsPath += "/Database";
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