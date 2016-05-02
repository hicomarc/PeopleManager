using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PeopleManager.WPF.BusinessObjects;
using PeopleManager.WPF.ViewModels;

namespace PeopleManager.WPF.Commands
{
    /// <summary>
    /// This command reloads the list of people.
    /// </summary>
    public class ReloadCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// The constructor initializes an instance of this class.
        /// </summary>
        /// <param name="mainWindowViewModel">The viewmodel that holds necessary data to act upon</param>
        public ReloadCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.mainWindowViewModel.PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        private void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.mainWindowViewModel.CurrentDataClient))
            {
                this.OnCanExecuteChanged();
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.mainWindowViewModel.CurrentDataClient != null;
        }

        public void Execute(object parameter)
        {
            try
            {
                var people = this.mainWindowViewModel.CurrentDataClient.GetPeopleWithAddresses();
                this.mainWindowViewModel.ResetPeopleList();
                foreach (var person in people)
                {
                    var personUI = new PersonUI(person);
                    this.mainWindowViewModel.People.Add(personUI);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
