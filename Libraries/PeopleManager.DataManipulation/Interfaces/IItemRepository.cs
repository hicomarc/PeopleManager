using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.DataManipulation.Interfaces
{
    /// <summary>
    /// This interface describes an item repository and exposes CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of item to handle</typeparam>
    public interface IItemRepository<T> : IDisposable
    {
        /// <summary>
        /// The implementation of this method should add an item to the data source.
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>The added item with updated properties</returns>
        T Add(T item);

        /// <summary>
        /// The implementation of this method should read all items from the data source.
        /// </summary>
        /// <returns>An enumeration of items found in the data source</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// The implementation of this method should return a single item that matches a given key.
        /// </summary>
        /// <param name="id">The key that should be searched within all items</param>
        /// <returns>The matching item</returns>
        T Get(Guid id);

        /// <summary>
        /// The implementation of this method should update a single item in the data source.
        /// </summary>
        /// <param name="item">The item to update</param>
        /// <returns>True, if the operation succeeds</returns>
        bool Update(T item);

        /// <summary>
        /// The implementation of this method should remove an item from the data source.
        /// </summary>
        /// <param name="item">The item to remove</param>
        void Remove(T item);
    }
}
