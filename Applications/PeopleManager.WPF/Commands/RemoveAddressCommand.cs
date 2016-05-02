using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// This command removes an address from a person.
    /// </summary>
    public class RemoveAddressCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// The constructor initializes an instance of this class.
        /// </summary>
        /// <param name="mainWindowViewModel">The viewmodel that holds necessary data to act upon</param>
        public RemoveAddressCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.mainWindowViewModel.PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        private void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.mainWindowViewModel.SelectedPerson))
            {
                this.OnCanExecuteChanged();
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.mainWindowViewModel.SelectedPerson != null && this.mainWindowViewModel.SelectedPerson.AddressesUI.Count > 1;
        }

        public void Execute(object parameter)
        {
            var address = parameter as AddressUI;
            this.mainWindowViewModel.SelectedPerson.RemoveAddress(address);
        }

        private void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
