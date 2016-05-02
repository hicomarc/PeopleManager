using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PeopleManager.Data.Model;

namespace PeopleManager.WPF.BusinessObjects
{
    public class PersonUI : Person, INotifyPropertyChanged, INotifyDataErrorInfo, IDisposable
    {
        private readonly Dictionary<string, bool> errors = new Dictionary<string, bool>();
        private ObservableCollection<Address> addresses;
        private bool disposedValue = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ObservableCollection<Address> AddressesUI { get; set; }

        public override List<Address> Addresses
        {
            get
            {
                return this.AddressesUI.ToList();
            }
        }

        public override string FirstName
        {
            get
            {
                return base.FirstName;
            }

            set
            {
                base.FirstName = value;
                this.OnPropertyChanged(nameof(this.FirstName));
                this.OnPropertyChanged(nameof(this.DisplayName));
                this.SetDirty();
            }
        }

        public override string LastName
        {
            get
            {
                return base.LastName;
            }

            set
            {
                base.LastName = value;
                this.OnPropertyChanged(nameof(this.LastName));
                this.OnPropertyChanged(nameof(this.DisplayName));
                this.SetDirty();
            }
        }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(this.FirstName) && string.IsNullOrEmpty(this.LastName))
                {
                    return "(Person)";
                }

                return $"{this.FirstName} {this.LastName}";
            }
        }

        public override int Age
        {
            get
            {
                return base.Age;
            }

            set
            {
                base.Age = value;
                this.OnPropertyChanged(nameof(this.Age));
                this.SetDirty();
            }
        }

        public bool IsDirty { get; private set; }

        public bool DisplayRemoveAddressButton { get { return this.AddressesUI.Count > 1; } }

        public bool HasErrors
        {
            get
            {
                var errorsDetected = this.GetErrors(string.Empty).OfType<ValidationResult>().Any(p => !p.IsValid);
                return errorsDetected;
            }
        }

        public PersonUI(Person person)
        {
            this.Id = person.Id;
            this.FirstName = person.FirstName;
            this.LastName = person.LastName;
            this.Age = person.Age;
            this.AddressesUI = new ObservableCollection<Address>();
            foreach (var address in person.Addresses)
            {
                var addressUI = new AddressUI(address);
                addressUI.PropertyChanged += AddressUI_PropertyChanged;
                this.AddressesUI.Add(addressUI);
            }

            this.IsDirty = false;
            this.OnPropertyChanged(nameof(this.IsDirty));
        }

        private void SetDirty()
        {
            this.IsDirty = true;
            this.OnPropertyChanged(nameof(this.IsDirty));
        }

        private void AddressUI_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var address = sender as AddressUI;
            if (address != null)
            {
                this.SetDirty();
            }
        }

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

        public IEnumerable GetErrors(string propertyName)
        {
            var result = new List<ValidationResult>();
            switch (propertyName)
            {
                case nameof(this.FirstName):
                    result.Add(string.IsNullOrEmpty(this.FirstName) ? new ValidationResult(false, "Firstname must have a value") : ValidationResult.ValidResult);
                    break;
                case nameof(this.LastName):
                    result.Add(string.IsNullOrEmpty(this.LastName) ? new ValidationResult(false, "Lastname must have a value") : ValidationResult.ValidResult);
                    break;
                case nameof(this.Age):
                    result.Add(this.Age <= 0 ? new ValidationResult(false, "Age must have a positive value") : ValidationResult.ValidResult);
                    break;
                case "":
                    result.Add(string.IsNullOrEmpty(this.FirstName) ? new ValidationResult(false, "Firstname must have a value") : ValidationResult.ValidResult);
                    result.Add(string.IsNullOrEmpty(this.LastName) ? new ValidationResult(false, "Lastname must have a value") : ValidationResult.ValidResult);
                    result.Add(this.Age <= 0 ? new ValidationResult(false, "Age must have a positive value") : ValidationResult.ValidResult);
                    break;
                default:
                    break;
            }

            result = result.Where(p => !p.IsValid).ToList();
            return result;
        }

        public AddressUI AddAddress()
        {
            var addressUI = new AddressUI(new Address());
            this.AddressesUI.Add(addressUI);
            addressUI.PropertyChanged += AddressUI_PropertyChanged;
            this.SetDirty();
            this.OnPropertyChanged(nameof(this.DisplayRemoveAddressButton));

            return addressUI;
        }

        public void RemoveAddress(AddressUI addressUI)
        {
            addressUI.PropertyChanged += AddressUI_PropertyChanged;
            this.AddressesUI.Remove(addressUI);
            this.SetDirty();
            this.OnPropertyChanged(nameof(this.DisplayRemoveAddressButton));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var addressUI in this.addresses.OfType<AddressUI>())
                    {
                        addressUI.PropertyChanged -= AddressUI_PropertyChanged;
                    }

                    this.addresses.Clear();
                    this.addresses = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
