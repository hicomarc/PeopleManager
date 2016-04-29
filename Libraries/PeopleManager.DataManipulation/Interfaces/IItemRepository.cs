using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.DataManipulation.Interfaces
{
    public interface IItemRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(Guid id);

        T Add(T item);

        void Remove(T item);

        bool Update(T item);
    }
}
