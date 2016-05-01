using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PeopleManager.Data.Model;

namespace PeopleManager.WPF.BusinessObjects
{
    public class AddressUI : Address, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, bool> errors = new Dictionary<string, bool>();

        public bool IsDirty { get; private set; }

        public override string City
        {
            get
            {
                return base.City;
            }

            set
            {
                base.City = value;
                this.OnPropertyChanged(nameof(this.City));
                this.SetDirty();
            }
        }

        public override string State
        {
            get
            {
                return base.State;
            }

            set
            {
                base.State = value;
                this.OnPropertyChanged(nameof(this.State));
                this.SetDirty();
            }
        }

        public override string Street
        {
            get
            {
                return base.Street;
            }

            set
            {
                base.Street = value;
                this.OnPropertyChanged(nameof(this.Street));
                this.SetDirty();
            }
        }

        public override int Zip
        {
            get
            {
                return base.Zip;
            }

            set
            {
                base.Zip = value;
                this.OnPropertyChanged(nameof(this.Zip));
                this.SetDirty();
            }
        }

        public bool HasErrors
        {
            get
            {
                var errorsDetected = this.GetErrors(string.Empty).OfType<ValidationResult>().Any(p => !p.IsValid);
                return errorsDetected;
            }
        }

        public AddressUI(Address address)
        {
            this.Id = address.Id;
            this.Street = address.Street;
            this.Zip = address.Zip;
            this.City = address.City;
            this.State = address.State;
            this.IsDirty = false;
            this.OnPropertyChanged(nameof(this.IsDirty));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName != nameof(this.IsDirty))
            {
                this.IsDirty = true;
                this.OnPropertyChanged(nameof(this.IsDirty));
                var errorsDetected = this.GetErrors(propertyName);

                var areErrorsDetected = errorsDetected.Cast<ValidationResult>().Any(p => !p.IsValid);
                var propertyNameForErrorDictionary = propertyName ?? string.Empty;
                if (!this.errors.ContainsKey(propertyNameForErrorDictionary))
                {
                    this.errors.Add(propertyNameForErrorDictionary, !areErrorsDetected);
                }

                if ((areErrorsDetected && !this.errors[propertyNameForErrorDictionary])
                    || (!areErrorsDetected && this.errors[propertyNameForErrorDictionary]))
                {
                    this.errors[propertyNameForErrorDictionary] = areErrorsDetected;
                    this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }
            }
        }

        private void SetDirty()
        {
            this.IsDirty = true;
            this.OnPropertyChanged(nameof(this.IsDirty));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            var result = new List<ValidationResult>();
            switch (propertyName)
            {
                case nameof(this.City):
                    result.Add(string.IsNullOrEmpty(this.City) ? new ValidationResult(false, "City must have a value") : ValidationResult.ValidResult);
                    break;
                case nameof(this.State):
                    result.Add(string.IsNullOrEmpty(this.State) ? new ValidationResult(false, "State must have a value") : ValidationResult.ValidResult);
                    break;
                case nameof(this.Street):
                    result.Add(string.IsNullOrEmpty(this.Street) ? new ValidationResult(false, "Street must have a value") : ValidationResult.ValidResult);
                    break;
                case "":
                    result.Add(string.IsNullOrEmpty(this.City) ? new ValidationResult(false, "City must have a value") : ValidationResult.ValidResult);
                    result.Add(string.IsNullOrEmpty(this.State) ? new ValidationResult(false, "State must have a value") : ValidationResult.ValidResult);
                    result.Add(string.IsNullOrEmpty(this.Street) ? new ValidationResult(false, "Street must have a value") : ValidationResult.ValidResult);
                    break;
                default:
                    break;
            }

            result = result.Where(p => !p.IsValid).ToList();
            return result;
        }
    }
}
