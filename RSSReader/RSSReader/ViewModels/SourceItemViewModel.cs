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
using RSSReader.Enums;
using RSSReader.EventsArgs;
using RSSReader.Repository;
using Xamarin.Forms;

namespace RSSReader.ViewModels {
    /// <summary>
    /// Class Source item ViewModel
    /// </summary>
    public class SourceItemViewModel : BaseForViewModel {
       RSSRepository repo = new RSSRepository();
        bool saveOnDatabase = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceViewModel"/> class.
        /// </summary>
        public SourceItemViewModel() {
            LoadDefaultData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceViewModel"/> class.
        /// </summary>
        /// <param name="sourceId">The Source identifier.</param>
        public SourceItemViewModel(int sourceId = 0, bool SaveOnDatabase = true) {
            saveOnDatabase = SaveOnDatabase;
            if (sourceId != 0)  {
                Id = sourceId;
            }

            IsBusy = true;
            LoadDefaultData();
            LoadData();
            IsBusy = false;
        }

        #region Load data
        /// <summary>
        /// Loads the default data.
        /// </summary>
        public void LoadDefaultData()  {
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        public void LoadData()  {
            Source source = null;
            if (Id != 0)  {
                source = repo.GetSource(Id);
                AssociateData(source);
            }
        }

        /// <summary>
        /// Associates source data from the database to the form
        /// </summary>
        /// <param name="source">The source.</param>
        private void AssociateData(Source source)  {
            if (source != null)  {
                Title = source.Title;
                SourceUrl = source.SourceUrl;
            }
        }
        #endregion
        #region Model

        /// <summary>
        /// Gets or sets the sourceurl.
        /// </summary>
        /// <value>The sourceurl.</value>
        public string SourceUrl
         {
            get  {
                return _sourceurl;
            }

            set  {
                if (_sourceurl != value)  {
                    _sourceurl = value;
                    OnPropertyChanged("SourceUrl");
                }
            }
        }
        private string _sourceurl;

        #endregion
        #region Events
        /// <summary>
        /// Save handler when it doesn't save on database
        /// </summary>
        public delegate void SaveNoDatabaseHandler(object sender, SaveSourceEventArgs e);

        /// <summary>
        /// Occurs when on save in memory
        /// </summary>
        public event SaveNoDatabaseHandler OnSaveInMemory;
        #endregion
        #region Commands
        ICommand _saveCommand = null;

        /// <summary>
        /// Gets the save source command.
        /// </summary>
        /// <value>The save source command.</value>
        public ICommand SaveSourceItem
         {
            get  {
                return _saveCommand ?? (_saveCommand =
                    new Command(async () => await ExecuteSaveSourceCommand()));
            }
        }

        /// <summary>
        /// Executes the save source command.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task ExecuteSaveSourceCommand()  {
                Validate();
                if (this.IsValid)  {
                    IsBusy = true;
                    SaveSourceOnDB();
                    IsBusy = false;
                }
                else  {
                    OnFormError(new FormErrorEventArgs("Source", "The form is not valid!"));
                }
        }

        /// <summary>
        /// Saves the source on database.
        /// </summary>
        private void SaveSourceOnDB()  {
            Source source = new Source();
            if (Id != 0)  {
                source = repo.GetSource(Id);
            }
            source.Title = Title;
            source.SourceUrl = SourceUrl;

            if (saveOnDatabase) {
               Id = repo.SaveSource(source);
                OnFormSave(new SaveEventArgs() { CreatedOrUpdatedId = Id });
            }
            else {
                SaveSourceEventArgs argsSave = new SaveSourceEventArgs();
                argsSave.SourceDetails = source;
                argsSave.SaveOnDatabase = saveOnDatabase;
                OnSaveInMemory(this, argsSave);
            }
        }

        /// <summary>
        /// Validates
        /// </summary>
        protected override void ValidateSelf()  {
            this.ValidationErrors.Clear();

            // validation example
            //if (fieldName == null)  {
            //    this.ValidationErrors["fieldName"] = "fieldName type is required";
            //}

            ShowErrors = this.ValidationErrors.Count > 0;
        }
        #endregion
    }
}
