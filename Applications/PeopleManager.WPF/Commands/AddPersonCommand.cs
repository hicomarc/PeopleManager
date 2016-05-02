using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PeopleManager.Data.Model;
using PeopleManager.WPF.BusinessObjects;
using PeopleManager.WPF.ViewModels;

namespace PeopleManager.WPF.Commands
{
    /// <summary>
    /// This command adds a person to the list of people.
    /// </summary>
    public class AddPersonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// The constructor initializes an instance of this class.
        /// </summary>
        /// <param name="mainWindowViewModel">The viewmodel that holds necessary data to act upon</param>
        public AddPersonCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.mainWindowViewModel.PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        private void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.mainWindowViewModel.People)
                || e.PropertyName == nameof(this.mainWindowViewModel.CurrentDataClient))
            {
                this.OnCanExecuteChanged();
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.mainWindowViewModel.CurrentDataClient != null && this.mainWindowViewModel.People != null;
        }

        public void Execute(object parameter)
        {
            var newPerson = new PersonUI(new Person { Addresses = new List<Address>(new List<Address> { new Address() }) });
            this.mainWindowViewModel.People.Add(newPerson);
            this.mainWindowViewModel.PeopleToAddWhenSaving.Add(newPerson);
            this.mainWindowViewModel.SelectedPerson = newPerson;
        }

        private void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
