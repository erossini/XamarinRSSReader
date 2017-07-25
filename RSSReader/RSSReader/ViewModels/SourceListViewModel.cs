using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSReader.Data;
using RSSReader.Helpers;
using RSSReader.Models;
using RSSReader.Repository;

namespace RSSReader.ViewModels {
    /// <summary>
    /// Class Source list ViewModel
    /// </summary>
    public class SourceListViewModel : BaseListViewModel {
        /// <summary>
        /// Gets or sets the source list.
        /// </summary>
        /// <value>The source list.</value>
        public ObservableCollection<Source> SourcesList { get; set; }

        /// <summary>
        /// The repository
        /// </summary>
        RSSRepository repo = new RSSRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesList"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public SourceListViewModel() : base() {
            LoadData();
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        public override void DeleteItem(int Id) {
            repo.DeleteSource(Id);
            LoadData();
        }

        /// <summary>
        /// Loads the data for this kind of source
        /// </summary>
        /// <param name="search">The search.</param>
        public override void LoadData(string search = "") {
            IsBusy = true;

            SourcesList = new ObservableCollection<Source>();
            List<Source> list = repo.GetSource();

            if (!string.IsNullOrEmpty(search)) {
                list = list.Where(l => (l.Title != null && l.Title.Contains(search))).ToList();
            }

            if (list != null) {
                foreach (Source source in list) {
                    Source model = source;
                    model.ImagePath = UIHelpers.SetOSImagePath("RSSIcon.png");
                    SourcesList.Add(model);
                    OnPropertyChanged("SourcesList");
                }
                ItemNumber = list.Count;
            }
            else {
                ItemNumber = 0;
            }

            if (ItemNumber == 0) {
                ShowEmpty = true;
                ShowListView = false;
                ItemNumberText = "No source found";
            }
            else {
                ShowEmpty = false;
                ShowListView = true;
                if (ItemNumber == 1) {
                    ItemNumberText = "1 source";
                }
                else {
                    ItemNumberText = $"{ItemNumber} sources";
                }
            }

            IsBusy = false;
        }

        public override void FilterTeams(string search, string category)
        {
            LoadData(search);
        }
    }
}
