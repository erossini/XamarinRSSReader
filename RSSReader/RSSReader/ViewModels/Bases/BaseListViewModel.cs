using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PSC.Xamarin.MvvmHelpers;
using RSSReader.Data;
using RSSReader.EventsArgs;
using RSSReader.Helpers;
using RSSReader.Repository;
using Xamarin.Forms;

namespace RSSReader.ViewModels
{
    public abstract class BaseListViewModel : BaseViewModel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseListViewModel"/> class.
        /// </summary>
        public BaseListViewModel()
        {
            LoadData();
        }

        /// <summary>
        /// Filters the teams.
        /// </summary>
        /// <param name="search">The search.</param>
        public abstract void FilterTeams(string search, string category);

        #region Errors 
        /// <summary>
        /// Form error handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ParamErrorEventArgs"/> instance containing the event data.</param>
        public delegate void ParamErrorHandler(object sender, ParamErrorEventArgs e);

        /// <summary>
        /// Occurs when parameter error
        /// </summary>
        public event ParamErrorHandler ParamError;

        /// <summary>
        /// Handles the <see cref="E:ParamError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ParamErrorEventArgs" /> instance containing the event data.</param>
        protected virtual void OnParamError(ParamErrorEventArgs e)
        {
            this.ParamError?.Invoke(this, e);
        }
        #endregion
        #region Model 
        /// <summary>
        /// The identifier
        /// </summary>
        private int _id;

        /// <summary>
        /// The show empty
        /// </summary>
        private bool _showEmpty;

        /// <summary>
        /// The show ListView
        /// </summary>
        private bool _showListView;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id
        {
            get {
                return _id;
            }

            set {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        public abstract void DeleteItem(int Id);

        /// <summary>
        /// Gets or sets the item number.
        /// </summary>
        /// <value>The item number.</value>
        public int ItemNumber { get; protected set; }

        /// <summary>
        /// Gets or sets the item number text.
        /// </summary>
        /// <value>The item number text.</value>
        public string ItemNumberText {
            get {
                return _itemNumberText;
            }
            set {
                if (_itemNumberText != value)
                {
                    _itemNumberText = value;
                    OnPropertyChanged("ItemNumberText");
                }
            }
        }
        private string _itemNumberText = "";

        /// <summary>
        /// Loads the data for this kind of item
        /// </summary>
        public abstract void LoadData(string search = "");

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            LoadData();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the list is empty
        /// </summary>
        /// <value><c>true</c> if {show empty}; otherwise, <c>false</c>.</value>
        public bool ShowEmpty
        {
            get {
                return _showEmpty;
            }
            set {
                if (_showEmpty != value)
                {
                    _showEmpty = value;
                    OnPropertyChanged("ShowEmpty");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the list is full
        /// </summary>
        /// <value><c>true</c> if {show ListView}; otherwise, <c>false</c>.</value>
        public bool ShowListView
        {
            get {
                return _showListView;
            }
            set {
                if (_showListView != value)
                {
                    _showListView = value;
                    OnPropertyChanged("ShowListView");
                }
            }
        }
        #endregion
        #region Command 
        private ICommand _refreshCommand;

        public ICommand RefreshCommand
        {
            get {
                return _refreshCommand ?? (_refreshCommand =
                new Command(async () => await RefreshCommandExecute()));
            }
        }

        private async Task RefreshCommandExecute()
        {
            LoadData();
        }
        #endregion
    }
}
