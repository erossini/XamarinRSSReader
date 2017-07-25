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

namespace RSSReader.Views  {
    public partial class SourceItem : ContentPage  {
        SourceItemViewModel vm = null;

        public SourceItem()  {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Source"/> class.
        /// </summary>
        /// <param name="SourceId">The source identifier.</param>
        public SourceItem(int sourceId = 0)  {
            InitializeComponent();
            LoadViewModel(sourceId);
        }

        /// <summary>
        /// Loads the view model.
        /// </summary>
        /// <param name="SourceId">The source identifier.</param>
        public void LoadViewModel(int sourceId = 0)  {
            if (vm == null)  {
                vm = new SourceItemViewModel(sourceId);
                vm.FormError += vm_FormError;
                vm.FormSave += vm_FormSave;
                vm.FormSaveError += vm_FormSaveError;
                vm.ParamError += vm_ParamError;
                BindingContext = vm;
            }
        }

        private void vm_ParamError(object sender, ParamErrorEventArgs e)  {
            //LogHelpers.Save(Enums.EventType.Error, "SourceItem Parameters error -> " + e.Message);
        }

        private void vm_FormSaveError(object sender, FormSaveErrorEventArgs e)  {
            //LogHelpers.Save(Enums.EventType.Error, "SourceItem Form Save Error -> " + e.Message);
        }

        private async void vm_FormSave(object sender, SaveEventArgs e)  {
            if (e.CreatedOrUpdatedId != 0)  {
                await Navigation.PopAsync();
            }
        }

        private void vm_FormError(object sender, FormErrorEventArgs e)  {
            //LogHelpers.Save(Enums.EventType.Error, "SourceItem Parameters error -> " + e.Message);
        }

        private void OnLoadingImages(object sender, LoadingEventArgs e)  {
            this.cvLoading.IsVisible = e.IsLoading;
        }
    }
}
