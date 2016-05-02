using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Data;
using PeopleManager.Data.Interfaces;
using PeopleManager.Data.Model;
using PeopleManager.DataManipulation.Interfaces;

namespace PeopleManager.DataManipulation
{
    /// <summary>
    /// This repository exposes CRUD operations for people
    /// </summary>
    public class PersonRepository : IItemRepository<Person>
    {
        private readonly IPeopleDataContext peopleDataContext;
        
        /// <summary>
        /// The constructor initializes a new instance of this class.
        /// <param name="peopleDataContext">The instance of the data context to use</param>
        /// </summary>
        public PersonRepository(IPeopleDataContext peopleDataContext)
        {
            this.peopleDataContext = peopleDataContext;
        }

        /// <summary>
        /// This method adds a person with its addresses to the data source.
        /// </summary>
        /// <param name="item">The person to add</param>
        /// <returns>The added person with updated properties</returns>
        public Person Add(Person item)
        {
            var result = this.peopleDataContext.People.Add(item);
            this.peopleDataContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// This method retrieves all people from the data source.
        /// </summary>
        /// <returns>An enumeration of people</returns>
        public IEnumerable<Person> GetAll()
        {
            var result = this.peopleDataContext.People.Include(nameof(Person.Addresses));
            return result;
        }

        /// <summary>
        /// This method searches the data source for a specific person.
        /// </summary>
        /// <param name="id">The id of the person to search</param>
        /// <returns>The matching person</returns>
        public Person Get(Guid id)
        {
            var result = this.peopleDataContext.People.Include(nameof(Person.Addresses)).FirstOrDefault(p => p.Id == id);
            return result;
        }

        /// <summary>
        /// This method updates a person with all its addresses in the data source
        /// </summary>
        /// <param name="item">The person to update</param>
        /// <returns>True, if the operation succeeds</returns>
        public bool Update(Person item)
        {
            var person = this.peopleDataContext.People.Find(item.Id);
            var entry = this.peopleDataContext.Entry(item);
            var dbEntry = this.peopleDataContext.Entry(person);

            dbEntry.CurrentValues.SetValues(item);

            dbEntry.Property(p => p.Age).IsModified = true;
            dbEntry.Property(p => p.FirstName).IsModified = true;
            dbEntry.Property(p => p.LastName).IsModified = true;

            foreach (var address in item.Addresses)
            {
                if (address.Id == Guid.Empty)
                {
                    this.peopleDataContext.Addresses.Add(address);
                    person.Addresses.Add(address);
                }
                else
                {
                    var dbAddress = this.peopleDataContext.Addresses.Find(address.Id);
                    var addressEntry = this.peopleDataContext.Entry(address);
                    var dbAddressEntry = this.peopleDataContext.Entry(dbAddress);
                    dbAddressEntry.Property(p => p.City).IsModified = true;
                    dbAddressEntry.Property(p => p.State).IsModified = true;
                    dbAddressEntry.Property(p => p.Street).IsModified = true;
                    dbAddressEntry.Property(p => p.Zip).IsModified = true;
                }
            }

            var addressesToRemove = new List<Address>();
            foreach (var address in person.Addresses)
            {
                if (!item.Addresses.Any(p => p.Id == address.Id))
                {
                    addressesToRemove.Add(address);
                }
            }

            this.peopleDataContext.Addresses.RemoveRange(addressesToRemove);

            this.peopleDataContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// This method removes a person from the data source
        /// </summary>
        /// <param name="item">The person to remove</param>
        public void Remove(Person item)
        {
            this.peopleDataContext.Addresses.RemoveRange(item.Addresses);
            this.peopleDataContext.People.Remove(item);
            this.peopleDataContext.SaveChanges();
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.peopleDataContext.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// This method disposes the resources of this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
