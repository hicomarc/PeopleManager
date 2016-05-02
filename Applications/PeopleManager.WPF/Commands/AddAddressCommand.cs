using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PeopleManager.WPF.ViewModels;

namespace PeopleManager.WPF.Commands
{
    /// <summary>
    /// This command adds an address to a person
    /// </summary>
    public class AddAddressCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// The constructor initializes an instance of this class.
        /// </summary>
        /// <param name="mainWindowViewModel">The viewmodel that holds necessary data to act upon</param>
        public AddAddressCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.mainWindowViewModel.PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        private void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.mainWindowViewModel.SelectedPerson))
            {
                this.OnCanExecuteChanged();
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.mainWindowViewModel.SelectedPerson != null;
        }

        public void Execute(object parameter)
        {
            this.mainWindowViewModel.SelectedPerson.AddAddress();
        }

        private void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
