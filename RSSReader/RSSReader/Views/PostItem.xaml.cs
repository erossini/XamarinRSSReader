using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSReader.EventsArgs;
using RSSReader.Helpers;
using RSSReader.Repository;
using RSSReader.ViewModels;
using Xamarin.Forms;
using RSSReader.Data;

namespace RSSReader.Views  {
    public partial class PostItem : ContentPage  {
        PostItemViewModel vm = null;

        public PostItem()  {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        /// <param name="PostId">The post identifier.</param>
        public PostItem(Post post)  {
            InitializeComponent();
            LoadViewModel(post);
        }

        /// <summary>
        /// Loads the view model.
        /// </summary>
        /// <param name="PostId">The post identifier.</param>
        public void LoadViewModel(Post post)  {
            if (vm == null)  {
                vm = new PostItemViewModel(post);
                vm.FormError += vm_FormError;
                vm.FormSave += vm_FormSave;
                vm.FormSaveError += vm_FormSaveError;
                vm.ParamError += vm_ParamError;
                BindingContext = vm;
            }
        }

        private void vm_ParamError(object sender, ParamErrorEventArgs e)  {
        }

        private void vm_FormSaveError(object sender, FormSaveErrorEventArgs e)  {
        }

        private async void vm_FormSave(object sender, SaveEventArgs e)  {
            if (e.CreatedOrUpdatedId != 0)  {
                await Navigation.PopAsync();
            }
        }

        private void vm_FormError(object sender, FormErrorEventArgs e)  {
        }

        private void OnLoadingImages(object sender, LoadingEventArgs e)  {
            this.cvLoading.IsVisible = e.IsLoading;
        }

        private void buttonFacebook_Clicked(object sender, EventArgs e)
        {
            string url = @"https://www.facebook.com/dialog/share?app_id=1473123496083579&href=" + System.Net.WebUtility.UrlEncode(vm.Link);
            Device.OpenUri(new Uri(url));
        }

        private void buttonTwitter_Clicked(object sender, EventArgs e)
        {
            string url = @"https://twitter.com/intent/tweet?text=" + System.Net.WebUtility.UrlEncode(vm.Title) + "&url=" + System.Net.WebUtility.UrlEncode(vm.Link);
            Device.OpenUri(new Uri(url));
        }

        private void buttonEmail_Clicked(object sender, EventArgs e)
        {
            string body = "Look this link " + System.Net.WebUtility.UrlEncode(vm.Link);
            Device.OpenUri(new Uri("mailto:?subject=Share&body=" + body));
        }
    }
}