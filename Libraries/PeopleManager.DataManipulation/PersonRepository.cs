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
    public class PersonRepository : IItemRepository<Person>, IDisposable
    {
        private readonly PeopleDataContext peopleDataContext;

        public PersonRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["peopleManagerEntities"].ConnectionString;
            this.peopleDataContext = new PeopleDataContext(connectionString);
        }
        public IEnumerable<Person> GetAll()
        {
            var result = this.peopleDataContext.People.Include(nameof(Person.Addresses));
            return result;
        }

        public Person Get(Guid id)
        {
            var result = this.peopleDataContext.People.Include(nameof(Person.Addresses)).FirstOrDefault(p => p.Id == id);
            return result;
        }

        public Person Add(Person item)
        {
            var result = this.peopleDataContext.People.Add(item);
            this.peopleDataContext.SaveChanges();
            return result;
        }

        public void Remove(Person item)
        {
            this.peopleDataContext.Addresses.RemoveRange(item.Addresses);
            this.peopleDataContext.People.Remove(item);
            this.peopleDataContext.SaveChanges();
        }

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

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
