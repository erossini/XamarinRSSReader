using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PSC.Xamarin.MvvmHelpers;
using RSSReader.Data;
using RSSReader.EventsArgs;
using RSSReader.Repository;
using Xamarin.Forms;

namespace RSSReader.ViewModels
{
    /// <summary>
    /// Class Post item ViewModel
    /// </summary>
    public class PostItemViewModel : BaseForViewModel
    {
        RSSRepository repo = new RSSRepository();
        bool saveOnDatabase = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostViewModel"/> class.
        /// </summary>
        public PostItemViewModel()
        {
            LoadDefaultData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostViewModel"/> class.
        /// </summary>
        /// <param name="postId">The Post identifier.</param>
        public PostItemViewModel(Post post, bool SaveOnDatabase = true)
        {
            saveOnDatabase = SaveOnDatabase;

            IsBusy = true;
            LoadDefaultData();
            LoadData(post);
            IsBusy = false;
        }

        #region Load data
        /// <summary>
        /// Loads the default data.
        /// </summary>
        public void LoadDefaultData()
        {
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        public void LoadData(Post post)
        {
            AssociateData(post);
        }

        /// <summary>
        /// Associates post data from the database to the form
        /// </summary>
        /// <param name="post">The post.</param>
        private void AssociateData(Post post)
        {
            if (post != null)
            {
                Title = post.Title;
                Description = post.Description;
                Category = post.Category;
                NewsSource = post.NewsSource;
                Link = post.Link;
                ImageUrl = post.ImageUrl;
                PubDate = post.PubDate;
            }
        }
        #endregion
        #region Model
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get {
                return _description;
            }

            set {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        private string _description;

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string Category
        {
            get {
                return _category;
            }

            set {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged("Category");
                }
            }
        }
        private string _category;

        /// <summary>
        /// Gets or sets the newssource.
        /// </summary>
        /// <value>The newssource.</value>
        public string NewsSource
        {
            get {
                return _newssource;
            }

            set {
                if (_newssource != value)
                {
                    _newssource = value;
                    OnPropertyChanged("NewsSource");
                }
            }
        }
        private string _newssource;

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public string Link
        {
            get {
                return _link;
            }

            set {
                if (_link != value)
                {
                    _link = value;
                    OnPropertyChanged("Link");
                }
            }
        }
        private string _link;

        /// <summary>
        /// Gets or sets the imageurl.
        /// </summary>
        /// <value>The imageurl.</value>
        public string ImageUrl
        {
            get {
                return _imageurl;
            }

            set {
                if (_imageurl != value)
                {
                    _imageurl = value;
                    OnPropertyChanged("ImageUrl");
                }
            }
        }
        private string _imageurl;

        /// <summary>
        /// Gets or sets the pubdate.
        /// </summary>
        /// <value>The pubdate.</value>
        public DateTime PubDate
        {
            get {
                return _pubdate;
            }

            set {
                if (_pubdate != value)
                {
                    _pubdate = value;
                    OnPropertyChanged("PubDate");
                }
            }
        }
        private DateTime _pubdate;

        #endregion
        #region Events
        /// <summary>
        /// Save handler when it doesn't save on database
        /// </summary>
        public delegate void SaveNoDatabaseHandler(object sender, SavePostEventArgs e);

        /// <summary>
        /// Occurs when on save in memory
        /// </summary>
        public event SaveNoDatabaseHandler OnSaveInMemory;
        #endregion
        #region Commands
        ICommand _saveCommand = null;

        /// <summary>
        /// Gets the save post command.
        /// </summary>
        /// <value>The save post command.</value>
        public ICommand SavePostItem
        {
            get {
                return _saveCommand ?? (_saveCommand =
                    new Command(async () => await ExecuteSavePostCommand()));
            }
        }

        /// <summary>
        /// Executes the save post command.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task ExecuteSavePostCommand()
        {
            Validate();
            if (this.IsValid)
            {
                IsBusy = true;
                SavePostOnDB();
                IsBusy = false;
            }
            else
            {
                OnFormError(new FormErrorEventArgs("Post", "The form is not valid!"));
            }
        }

        /// <summary>
        /// Saves the post on database.
        /// </summary>
        private void SavePostOnDB()
        {
            Post post = new Post();
            if (Id != 0)
            {
                post = repo.GetPost(Id);
            }
            post.Title = Title;
            post.Description = Description;
            post.Category = Category;
            post.NewsSource = NewsSource;
            post.Link = Link;
            post.ImageUrl = ImageUrl;
            post.PubDate = PubDate;

            if (saveOnDatabase)
            {
                Id = repo.SavePost(post);
                OnFormSave(new SaveEventArgs() { CreatedOrUpdatedId = Id });
            }
            else
            {
                SavePostEventArgs argsSave = new SavePostEventArgs();
                argsSave.PostDetails = post;
                argsSave.SaveOnDatabase = saveOnDatabase;
                OnSaveInMemory(this, argsSave);
            }
        }

        /// <summary>
        /// Validates
        /// </summary>
        protected override void ValidateSelf()
        {
            this.ValidationErrors.Clear();

            // validation example
            //if (SelectedPost == null)  {
            //    this.ValidationErrors["SelectedPost"] = "Post type is required";
            //}

            ShowErrors = this.ValidationErrors.Count > 0;
        }
        #endregion
    }
}
