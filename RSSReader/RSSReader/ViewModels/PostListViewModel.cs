using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSReader.Data;
using RSSReader.Repository;
using RSSReader.Web;
using RSSReader.Extensions;
using Xamarin.Forms;

namespace RSSReader.ViewModels
{
    /// <summary>
    /// Class Post list ViewModel
    /// </summary>
    public class PostListViewModel : BaseListViewModel
    {
        /// <summary>
        /// The list
        /// </summary>
        public List<Post> list;

        /// <summary>
        /// Gets or sets the post list.
        /// </summary>
        /// <value>The post list.</value>
        public ObservableCollection<Post> PostsList { get; set; }

        /// <summary>
        /// Gets or sets the category list.
        /// </summary>
        /// <value>The category list.</value>
        public ObservableCollection<string> CategoryList { get; set; }

        /// <summary>
        /// The repository
        /// </summary>
        RSSRepository repo = new RSSRepository();

        /// <summary>
        /// The show save for later
        /// </summary>
        public bool showSaveForLater = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostViewModel" /> class.
        /// </summary>
        public PostListViewModel() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostListViewModel"/> class.
        /// </summary>
        /// <param name="showLater">if set to <c>true</c> [show later].</param>
        public PostListViewModel(bool showLater)
        {
            showSaveForLater = showLater;
            LoadData();
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        public override void DeleteItem(int Id)
        {
            repo.DeletePost(Id);
            LoadData();
        }

        /// <summary>
        /// Clears the data.
        /// </summary>
        public void ClearData()
        {
            CategoryList.RemoveAll();
            PostsList.RemoveAll();
            OnPropertyChanged("CategoryList");
            OnPropertyChanged("PostsList");
        }

        /// <summary>
        /// Loads the data for this kind of post
        /// </summary>
        /// <param name="search">The search.</param>
        public override async void LoadData(string search = "")
        {
            IsBusy = true;

            CategoryList = new ObservableCollection<string>();
            CategoryList.Add("All");

            PostsList = new ObservableCollection<Post>();
            list = new List<Post>();

            if (!showSaveForLater)
            {
                List<Source> sources = repo.GetSource();

                RSSClient client = new RSSClient();
                foreach (Source source in sources)
                    if (!string.IsNullOrEmpty(source.SourceUrl))
                        list.AddRange(await client.CallUrlAsync(source.SourceUrl));
            }
            else
            {
                list = repo.GetPost();
            }

            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(l => (l.Title != null && l.Title.Contains(search))).ToList();
            }

            CreateListForUI(list);

            IsBusy = false;
        }

        /// <summary>
        /// Filters the teams.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="category">The category.</param>
        public override void FilterTeams(string search, string category)
        {
            if (list != null && list.Count > 0)
            {
                ClearData();

                List<Post> newlist = list;
                if (!string.IsNullOrEmpty(search))
                    newlist = list.Where(l => l.Title != null && l.Title.ToLower().Contains(search.ToLower().Trim())).ToList();
                if (!string.IsNullOrEmpty(category))
                    newlist = list.Where(l => l.Category != null && l.Category.ToLower().Contains(search.ToLower())).ToList();

                CreateListForUI(newlist);
            }
        }

        /// <summary>
        /// Creates the list for UI.
        /// </summary>
        /// <param name="newlist">The list.</param>
        public void CreateListForUI(List<Post> newlist)
        {
            if (newlist != null)
            {
                foreach (Post post in newlist)
                {
                    post.CircleColor = Color.FromHex("#FF6600");
                    PostsList.Add(post);
                    OnPropertyChanged("PostsList");
                }
                ItemNumber = newlist.Count;

                foreach (string s in newlist.Select(l => l.Category).Distinct())
                    if (!string.IsNullOrEmpty(s))
                        CategoryList.Add(s);
            }
            else
            {
                ItemNumber = 0;
                OnPropertyChanged("PostsList");
            }

            if (ItemNumber == 0)
            {
                ShowEmpty = true;
                ShowListView = false;
                ItemNumberText = "No post found";
            }
            else
            {
                ShowEmpty = false;
                ShowListView = true;
                if (ItemNumber == 1)
                {
                    ItemNumberText = "1 post";
                }
                else
                {
                    ItemNumberText = $"{ItemNumber} posts";
                }
            }
        }

        /// <summary>
        /// Gets the post from list.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Post.</returns>
        public Post GetPostFromList(int id)
        {
            Post post = null;

            if (list.Count > 0)
            {
                post = list.Where(l => l.Id == id).FirstOrDefault();
            }

            return post;
        }

        /// <summary>
        /// Saves the post.
        /// </summary>
        /// <param name="post">The post.</param>
        public async Task SavePost(Post post)
        {
            if (!repo.Exists(post.Link))
            {
                PageDownload pd = new PageDownload();

                post.HtmlPost = await pd.Start(post.Link);
                int result = repo.SavePost(post);
            }
        }
    }
}