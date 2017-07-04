using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSC.Xamarin.MvvmHelpers;
using RSSReader.EventsArgs;

namespace RSSReader.ViewModels
{
    /// <summary>
    /// Base for ViewModel
    /// </summary>
    /// <seealso cref="PSC.Xamarin.MvvmHelpers.BaseViewModel" />
    public abstract class BaseForViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseForViewModel"/> class.
        /// </summary>
        protected BaseForViewModel()
        {
            this.ValidationErrors = new Dictionary<string, string>();
        }

        #region Model
        private int _id;

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
        #endregion
        #region Validation 
        #region Errors
        private string _errorDescription;
        private bool _showErrors;

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>The error description.</value>
        public string ErrorDescription
        {
            get {
                return _errorDescription;
            }
            set {
                if (_errorDescription != value)
                {
                    _errorDescription = value;
                    OnPropertyChanged("ErrorDescription");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:myInventories.ViewModels.BaseGalleryImage"/> show errors.
        /// </summary>
        /// <value><c>true</c> if show errors; otherwise, <c>false</c>.</value>
        public bool ShowErrors
        {
            get {
                return _showErrors;
            }
            set {
                if (_showErrors != value)
                {
                    _showErrors = value;
                    OnPropertyChanged("ShowErrors");

                    ErrorDescription = "";
                    if (this.ValidationErrors.Count > 0)
                    {
                        ErrorDescription = "I can't save! There are some errors in the form:";
                        foreach (KeyValuePair<string, string> s in this.ValidationErrors)
                        {
                            if (!string.IsNullOrEmpty(ErrorDescription))
                                ErrorDescription += "\r\n";
                            ErrorDescription += "  - " + s.Value.Trim();
                        }
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// Gets or sets the validation errors.
        /// </summary>
        /// <value>The validation errors.</value>
        public Dictionary<string, string> ValidationErrors { get; set; }

        /// <summary>
        /// Returns true if there is not error is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get {
                return this.ValidationErrors.Count < 1;
            }
            private set {
                IsValid = value;
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            this.ValidationErrors.Clear();
            this.ValidateSelf();
            this.OnPropertyChanged("IsValid");
            this.OnPropertyChanged("ValidationErrors");
        }

        /// <summary>
        /// Validates
        /// </summary>
        protected abstract void ValidateSelf();
        #endregion
        #region Events
        /// <summary>
        /// Form error handler.
        /// </summary>
        public delegate void FormErrorHandler(object sender, FormErrorEventArgs e);

        /// <summary>
        /// Occurs when {form error}.
        /// </summary>
        public event FormErrorHandler FormError;

        /// <summary>
        /// Save handler.
        /// </summary>
        public delegate void SaveHandler(object sender, SaveEventArgs e);

        /// <summary>
        /// Occurs when {form save}.
        /// </summary>
        public event SaveHandler FormSave;

        /// <summary>
        /// Form error handler.
        /// </summary>
        public delegate void FormSaveErrorHandler(object sender, FormSaveErrorEventArgs e);

        /// <summary>
        /// Occurs when on form error.
        /// </summary>
        public event FormSaveErrorHandler FormSaveError;


        /// <summary>
        /// Form error handler.
        /// </summary>
        public delegate void ParamErrorHandler(object sender, ParamErrorEventArgs e);

        /// <summary>
        /// Occurs when parameter error
        /// </summary>
        public event ParamErrorHandler ParamError;

        /// <summary>
        /// Handles the <see cref="E:FormError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="FormErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnFormError(FormErrorEventArgs e)
        {
            this.FormError?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the <see cref="E:FormSaveError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="FormSaveErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnFormSaveError(FormSaveErrorEventArgs e)
        {
            this.FormSaveError?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the <see cref="E:ParamError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ParamErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnParamError(ParamErrorEventArgs e)
        {
            this.ParamError?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the <see cref="E:FormSave" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SaveEventArgs"/> instance containing the event data.</param>
        protected virtual void OnFormSave(SaveEventArgs e)
        {
            this.FormSave?.Invoke(this, e);
        }
        #endregion
    }
}
