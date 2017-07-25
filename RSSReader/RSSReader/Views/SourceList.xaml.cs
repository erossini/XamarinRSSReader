using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSReader.Helpers;
using RSSReader.Models;
using RSSReader.ViewModels;
using Xamarin.Forms;
using RSSReader.Data;

namespace RSSReader.Views {
    public partial class SourceList : ContentPage {
        SourceListViewModel vm = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Source"/> class.
        /// </summary>
        /// <param name="SourceId">The source identifier.</param>
        public SourceList() {
            InitializeComponent();

            LoadViewModel();
        }

        /// <summary>
        /// Loads the view model.
        /// </summary>
        /// <param name="SourceId">The source identifier.</param>
        private void LoadViewModel() {
            if (vm == null) {
                vm = new SourceListViewModel();
                vm.ParamError += vm_ParamError;
                BindingContext = vm;
            }
            else {
                vm.LoadData();
            }
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            vm = null;
            LoadViewModel();
        }

        private void vm_ParamError(object sender, EventsArgs.ParamErrorEventArgs e) {
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            var source = ((ListView)sender).SelectedItem as Source;
            if (source == null)
                return;

            await Navigation.PushAsync(new SourceItem(source.Id), true);
        }

        public async void OnClickedNew(object sender, EventArgs e) {
            await Navigation.PushAsync(new SourceItem(0), true);
        }

        public async void OnDelete(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            bool answer = await DisplayAlert("AppName", "Are you sure you want to delete it?", "Yes", "Cancel");

            if (answer) {
                vm.DeleteItem((int)mi.CommandParameter);
                vm.Refresh();
            }
        }

        public void OnEdit(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            Navigation.PushAsync(new SourceItem(  Convert.ToInt32(mi.CommandParameter)), true);
        }

        public void OnTextChanged(object sender, EventArgs e) {
            string search = "";
            if (!string.IsNullOrEmpty(this.searchBar.Text))
                search = this.searchBar.Text.Trim();
            vm.FilterTeams(search, "");
        }
    }
}