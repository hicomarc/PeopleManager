using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PeopleManager.WPF.ViewModels;

namespace PeopleManager.WPF.Commands
{
    /// <summary>
    /// This command saves all changes made to people and their addresses.
    /// </summary>
    public class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// The constructor initializes an instance of this class.
        /// </summary>
        /// <param name="mainWindowViewModel">The viewmodel that holds necessary data to act upon</param>
        public SaveCommand(MainWindowViewModel mainWindowViewModel)
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
                foreach (var person in this.mainWindowViewModel.PeopleToUpdateWhenSaving)
                {
                    this.mainWindowViewModel.CurrentDataClient.UpdatePerson(person);
                }

                foreach (var newPerson in this.mainWindowViewModel.PeopleToAddWhenSaving)
                {
                    this.mainWindowViewModel.CurrentDataClient.AddPerson(newPerson);
                }

                foreach (var removedPerson in this.mainWindowViewModel.PeopleToRemoveWhenSaving)
                {
                    this.mainWindowViewModel.CurrentDataClient.RemovePerson(removedPerson);
                }

                if (this.mainWindowViewModel.Reload.CanExecute(null))
                {
                    this.mainWindowViewModel.Reload.Execute(null);
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
