using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.WPF.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> collectionToAdd)
        {
            foreach (var item in collectionToAdd)
            {
                collection.Add(item);
            }
        }
    }
}
