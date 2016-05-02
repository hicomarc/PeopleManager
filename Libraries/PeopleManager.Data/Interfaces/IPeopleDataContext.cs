using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Data.Model;

namespace PeopleManager.Data.Interfaces
{
    /// <summary>
    /// An interface for the data context of database entities
    /// </summary>
    public interface IPeopleDataContext : IDisposable
    {
        /// <summary>
        /// The set to store addresses of people
        /// </summary>
        DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// The set to store the people
        /// </summary>
        DbSet<Person> People { get; set; }

        /// <summary>
        /// The implementation of this method should persist changes to the database.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        
        /// <summary>
        /// The implementation of this method should return an entry for a given entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="entity">The entity to retrieve an entry for</param>
        /// <returns>The typed entry for the entity</returns>
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
