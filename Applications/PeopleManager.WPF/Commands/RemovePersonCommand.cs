using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PeopleManager.WPF.ViewModels;

namespace PeopleManager.WPF.Commands
{
    public class RemovePersonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MainWindowViewModel mainWindowViewModel;

        public RemovePersonCommand(MainWindowViewModel mainWindowViewModel)
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
            var personToRemove = this.mainWindowViewModel.SelectedPerson;

            this.mainWindowViewModel.People.Remove(personToRemove);
            if (this.mainWindowViewModel.PeopleToAddWhenSaving.Contains(personToRemove))
            {
                this.mainWindowViewModel.PeopleToAddWhenSaving.Remove(personToRemove);
            }
            else
            {
                this.mainWindowViewModel.PeopleToRemoveWhenSaving.Add(personToRemove);
            }
        }

        private void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
