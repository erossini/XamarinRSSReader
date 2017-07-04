using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSReader.Helpers;
using RSSReader.ViewModels;
using Xamarin.Forms;
using RSSReader.Data;

namespace RSSReader.Views {
    /// <summary>
    /// Class PostList.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class PostList : ContentPage {
        PostListViewModel vm = null;
        bool showLater = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        public PostList() {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the view model.
        /// </summary>
        /// <param name="PostId">The post identifier.</param>
        private void LoadViewModel() {
            if (vm == null) {
                vm = new PostListViewModel(showLater);
                vm.ParamError += vm_ParamError;
                BindingContext = vm;
            }
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>To be added.</remarks>
        protected override void OnAppearing() {
            base.OnAppearing();
            LoadViewModel();
        }

        /// <summary>
        /// Handles the ParamError event of the vm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventsArgs.ParamErrorEventArgs"/> instance containing the event data.</param>
        private void vm_ParamError(object sender, EventsArgs.ParamErrorEventArgs e) {
        }

        /// <summary>
        /// Handles the ItemTapped event of the Handle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemTappedEventArgs"/> instance containing the event data.</param>
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        /// <summary>
        /// Handles the ItemSelected event of the Handle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            var post = ((ListView)sender).SelectedItem as Post;
            if (post == null)
                return;

            await Navigation.PushAsync(new PostItem(post), true);
        }

        /// <summary>
        /// Handles the <see cref="E:SaveForLater" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void OnSaveForLater(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            vm.SavePost(vm.GetPostFromList(Convert.ToInt32(mi.CommandParameter)));
        }

        /// <summary>
        /// Handles the <see cref="E:TextChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void OnTextChanged(object sender, EventArgs e) {
            Search();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the pickerCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pickerCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        private void Search()
        {
            string search = "";
            if (!string.IsNullOrEmpty(this.searchBar.Text))
                search = this.searchBar.Text.Trim();

            string category = "";
            if (this.pickerCategory.SelectedIndex >= 0)
                category = this.pickerCategory.SelectedItem.ToString();
            if (category == "All")
                category = "";

            vm.FilterTeams(search, category);
        }

        /// <summary>
        /// Handles the Clicked event of the buttonShow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonShow_Clicked(object sender, EventArgs e)
        {
            if (!showLater)
            {
                showLater = true;
                this.buttonShow.Text = "Show all";
            }
            else
            {
                showLater = false;
                this.buttonShow.Text = "Show post saved";
            }

            vm.showSaveForLater = showLater;
            vm.Refresh();
        }
    }
}
