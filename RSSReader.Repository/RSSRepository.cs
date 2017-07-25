using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RSSReader.Data;
using RSSReader.Interfaces;

namespace RSSReader.Repository
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class RSSRepository : IDisposable
    {
        RSSDatabase db = null;

        public RSSRepository()
        {
            db = new RSSDatabase();
        }

        public void Dispose() { }

        /// <summary>
        /// Existses the specified link.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Exists(string link)
        {
            Expression<Func<Post, bool>> func = f => f.Link.Contains(link);
            List<Post> result = GetPost(func);
            return result.Count != 0;
        }

        #region Post
        /// <summary>
        /// Gets the post
        /// </summary>
        /// <returns></returns>
        public List<Post> GetPost()
        {
            return db.GetItems<Post>();
        }

        /// <summary>
        /// Gets the post
        /// </summary>)
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Post GetPost(int id)
        {
            return db.GetItem<Post>(id);
        }

        /// <summary>
        /// Saves the post
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public int SavePost(Post item)
        {
            db.SaveItem<Post>(item);
            return item.Id;
        }

        /// <summary>
        /// Deletes the post
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int DeletePost(int id)
        {
            return db.DeleteItem<Post>(id);
        }

        /// <summary>
        /// Gets list of post by function.
        /// </summary>
        /// <param name="func">Function</param>
        /// <returns>The list of post.</returns>
        public List<Post> GetPost(Expression<Func<Post, bool>> func)
        {
            return db.GetItems<Post>(func);
        }
        #endregion
        #region Source
        /// <summary>
        /// Gets the source
        /// </summary>
        /// <returns></returns>
        public List<Source> GetSource()
        {
            return db.GetItems<Source>();
        }

        /// <summary>
        /// Gets the source
        /// </summary>)
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Source GetSource(int id)
        {
            return db.GetItem<Source>(id);
        }

        /// <summary>
        /// Saves the source
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public int SaveSource(Source item)
        {
            db.SaveItem<Source>(item);
            return item.Id;
        }

        /// <summary>
        /// Deletes the source
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int DeleteSource(int id)
        {
            return db.DeleteItem<Source>(id);
        }

        /// <summary>
        /// Gets list of source by function.
        /// </summary>
        /// <param name="func">Function</param>
        /// <returns>The list of source.</returns>
        public List<Source> GetSource(Expression<Func<Source, bool>> func)
        {
            return db.GetItems<Source>(func);
        }
        #endregion
    }
}
