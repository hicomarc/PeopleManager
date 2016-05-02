using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PeopleManager.Data.Model;
using PeopleManager.DataAccess.DataClients;
using PeopleManager.DataAccess.Interfaces;
using PeopleManager.WPF.BusinessObjects;
using PeopleManager.WPF.Commands;

namespace PeopleManager.WPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private bool suppressDataSourceChoicePropertyChangedEvent = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PersonUI> People { get; private set; } = new ObservableCollection<PersonUI>();

        public ObservableCollection<PersonUI> PeopleToAddWhenSaving { get; private set; } = new ObservableCollection<PersonUI>();

        public ObservableCollection<PersonUI> PeopleToRemoveWhenSaving { get; private set; } = new ObservableCollection<PersonUI>();

        private PersonUI selectedPerson;
        public PersonUI SelectedPerson
        {
            get { return this.selectedPerson; }
            set
            {
                this.selectedPerson = value;
                this.OnPropertyChanged(nameof(this.SelectedPerson));
            }
        }

        private IDataClient currentDataClient;
        public IDataClient CurrentDataClient
        {
            get { return this.currentDataClient; }
            set
            {
                var v = value;
                if (v != this.currentDataClient)
                {
                    this.currentDataClient = value;
                    this.OnPropertyChanged(nameof(this.CurrentDataClient));
                }
            }
        }

        private List<DataSourceChoice> dataSourceChoices;
        public List<DataSourceChoice> DataSourceChoices { get { return this.dataSourceChoices; } }

        public MainWindowViewModel()
        {
            this.selectedPerson = null;
            this.People.CollectionChanged += People_CollectionChanged;
            this.People.CollectionChanged += SomePeople_CollectionChanged;
            this.PeopleToAddWhenSaving.CollectionChanged += SomePeople_CollectionChanged;
            this.PeopleToRemoveWhenSaving.CollectionChanged += SomePeople_CollectionChanged;

            var webApi2BaseUrl = Properties.Settings.Default.WebApi2DataClientUrl.TrimEnd("/".ToCharArray()) + "/";
            var wcfBaseUrl = Properties.Settings.Default.WCFDataClientUrl.TrimEnd("/".ToCharArray()) + "/";
            this.dataSourceChoices = new List<DataSourceChoice>();
            this.dataSourceChoices.Add(new DataSourceChoice(new WebApi2DataClient(webApi2BaseUrl)));
            this.dataSourceChoices.Add(new DataSourceChoice(new WCFDataClient(wcfBaseUrl)));
            foreach (var dataSourceChoice in this.dataSourceChoices)
            {
                dataSourceChoice.PropertyChanged += DataSourceChoice_PropertyChanged;
            }
        }

        public MainWindowViewModel(List<IDataClient> dataClients)
        {
            this.selectedPerson = null;
            this.People.CollectionChanged += People_CollectionChanged;
            this.People.CollectionChanged += SomePeople_CollectionChanged;
            this.PeopleToAddWhenSaving.CollectionChanged += SomePeople_CollectionChanged;
            this.PeopleToRemoveWhenSaving.CollectionChanged += SomePeople_CollectionChanged;

            this.dataSourceChoices = new List<DataSourceChoice>(dataClients.Select(p =>
            {
                var dataSourceChoice = new DataSourceChoice(p);
                dataSourceChoice.PropertyChanged += DataSourceChoice_PropertyChanged;
                return dataSourceChoice;
            }));
        }

        public List<PersonUI> PeopleToUpdateWhenSaving => this.People.Except(this.PeopleToAddWhenSaving).Except(this.PeopleToRemoveWhenSaving).Where(p => p.IsDirty).ToList();

        public int PeopleToAddCount => this.PeopleToAddWhenSaving.Count;

        public int PeopleToRemoveCount => this.PeopleToRemoveWhenSaving.Count;

        public int PeopleToUpdateCount => this.PeopleToUpdateWhenSaving.Count;

        public ICommand Reload => new ReloadCommand(this);

        public ICommand AddAddress => new AddAddressCommand(this);

        public ICommand RemoveAddress => new RemoveAddressCommand(this);

        public ICommand AddPerson => new AddPersonCommand(this);

        public ICommand RemovePerson => new RemovePersonCommand(this);

        public ICommand Save => new SaveCommand(this);

        public void ResetPeopleList()
        {
            foreach (var oldPerson in this.People)
            {
                oldPerson.PropertyChanged -= Person_PropertyChanged;
            }

            this.People.Clear();
            this.PeopleToAddWhenSaving.Clear();
            this.PeopleToRemoveWhenSaving.Clear();
            this.RaiseSomePeopleCollectionChanged();
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var newPerson in e.NewItems.OfType<PersonUI>())
                {
                    newPerson.PropertyChanged += Person_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var oldPerson in e.OldItems.OfType<PersonUI>())
                {
                    oldPerson.PropertyChanged -= Person_PropertyChanged;
                }
            }
        }

        private void Person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PersonUI.IsDirty))
            {
                this.RaiseSomePeopleCollectionChanged();
            }
            else if (e.PropertyName == nameof(PersonUI.DisplayRemoveAddressButton))
            {
                this.OnPropertyChanged(nameof(this.SelectedPerson));
            }
        }

        private void SomePeople_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.RaiseSomePeopleCollectionChanged();
        }

        private void RaiseSomePeopleCollectionChanged()
        {
            this.OnPropertyChanged(nameof(this.PeopleToAddCount));
            this.OnPropertyChanged(nameof(this.PeopleToRemoveCount));
            this.OnPropertyChanged(nameof(this.PeopleToUpdateCount));
        }

        private void DataSourceChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataSourceChoice.IsSelected) && !suppressDataSourceChoicePropertyChangedEvent)
            {
                this.suppressDataSourceChoicePropertyChangedEvent = true;
                var changedDataSourceChoice = (DataSourceChoice)sender;
                this.currentDataClient = changedDataSourceChoice.DataClient;

                foreach (var dataSourceChoice in this.dataSourceChoices)
                {
                    var isSelected = dataSourceChoice == changedDataSourceChoice;
                    if (isSelected != dataSourceChoice.IsSelected)
                    {
                        dataSourceChoice.IsSelected = isSelected;
                    }
                }

                this.OnPropertyChanged(nameof(this.CurrentDataClient));
                this.ResetPeopleList();
                this.suppressDataSourceChoicePropertyChangedEvent = false;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
