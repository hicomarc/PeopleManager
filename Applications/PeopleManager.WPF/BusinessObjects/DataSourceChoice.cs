using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.DataAccess.Interfaces;

namespace PeopleManager.WPF.BusinessObjects
{
    public class DataSourceChoice : INotifyPropertyChanged
    {
        private readonly IDataClient dataClient;
        public IDataClient DataClient
        {
            get { return this.dataClient; }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged(nameof(this.IsSelected));
            }
        }

        public DataSourceChoice(IDataClient dataClient)
        {
            this.dataClient = dataClient;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
