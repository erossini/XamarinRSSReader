using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RSSReader.Enums;
using System.IO;

namespace RSSReader.Droid.Helpers
{
    public class FolderUtilities
    {
        /// <summary>
		/// Creates the application folders.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns>System.String.</returns>
		public bool CreateAppFolders(out FolderError error)
        {
            bool rtn = false;
            error = VerifyPermission();

            if (error == FolderError.None)
            {
                // check if the main folder exists or create it
                string path = GetAccessibleFolderFromUser(out error);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // create main folders
                if (!Directory.Exists(path + "/Database"))
                    Directory.CreateDirectory(path + "/Database");

                rtn = true;
            }

            return rtn;
        }

        /// <summary>
        /// Gets the accessible folder from user via FileExplorer.
        /// This folder is useful when you want to inspect files and directory or move files between a device and a laptop.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>System.String.</returns>
        public string GetAccessibleFolderFromUser(out FolderError error, bool verify = false)
        {
            string path = GetAppFolder();

            if (verify)
            {
                error = VerifyPermission();
            }
            else
            {
                // set no error for default
                error = FolderError.None;
            }

            return path;
        }

        /// <summary>
        /// Gets the app folder.
        /// </summary>
        /// <returns>The app folder.</returns>
        public string GetAppFolder()
        {
            Java.IO.File storage = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);
            return storage.ToString() + "/RSSReader";
        }

        /// <summary>
        /// Loads the text.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>System.String.</returns>
        public string LoadText(string filename)
        {
            FolderError error = FolderError.None;
            var documentsPath = GetAccessibleFolderFromUser(out error);
            var filePath = Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }

        /// <summary>
        /// Saves the text.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="text">The text.</param>
        public void SaveText(string filename, string text)
        {
            FolderError error = FolderError.None;
            string documentsPath = GetAccessibleFolderFromUser(out error);
            var filePath = Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }

        /// <summary>
        /// Verifies the permission.
        /// </summary>
        /// <returns>FolderError.</returns>
        public FolderError VerifyPermission()
        {
            FolderError error = FolderError.None;
            string path = GetAccessibleFolderFromUser(out error);

            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                error = FolderError.PermissionErrorOnDirectories;
            }

            if (error == FolderError.None)
            {
                try
                {
                    SaveText(path + "/RSSReader.txt", "Hello, permissions are ok!");
                }
                catch (Exception ex)
                {
                    error = FolderError.PermissionErrorOnFileCreate;
                }
            }

            if (error == FolderError.None)
            {
                try
                {
                    string txt = LoadText(path + "/RSSReader.txt");
                }
                catch (Exception ex)
                {
                    error = FolderError.PermissionErrorOnFileRead;
                }
            }

            try
            {
                File.Delete(path + "/RSSReader.txt");
            }
            catch (Exception ex) { }

            return error;
        }
    }
}