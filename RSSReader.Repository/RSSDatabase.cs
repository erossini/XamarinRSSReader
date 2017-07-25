using RSSReader.Data;
using RSSReader.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xamarin.Forms;

namespace RSSReader.Repository
{
    public class RSSDatabase
    {
        /// <summary>
        /// The database
        /// </summary>
        SQLiteConnection database;

        /// <summary>
        /// The locker
        /// </summary>
        static object locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public RSSDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            InitDatabase();
        }

        /// <summary>
        /// Init the database.
        /// </summary>
        public void InitDatabase()
        {
            if (database != null)
            {
                database.CreateTable<Post>();
                database.CreateTable<Source>();
            }
        }

        /// <summary>
        /// Gets the items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>All items from a table without deleted records</returns>
        public List<T> GetItems<T>() where T : ITableEntityRSSReader, new()
        {
            lock (locker)
            {
                return (from i in database.Table<T>()
                        select i).ToList();
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> GetItems<T>(Expression<Func<T, bool>> func) where T : ITableEntityRSSReader, new()
        {
            lock (locker)
            {
                return database.Table<T>().Where(func).ToList();
            }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetItem<T>(int id) where T : ITableEntityRSSReader, new()
        {
            lock (locker)
            {
                return database.Table<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public int SaveItem<T>(T item) where T : ITableEntityRSSReader
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int DeleteItem<T>(int id) where T : ITableEntityRSSReader, new()
        {
            lock (locker)
            {
                T tmpRecord = GetItem<T>(id);
                return database.Delete<T>(id);
            }
        }
    }
}