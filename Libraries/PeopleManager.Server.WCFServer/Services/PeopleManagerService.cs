using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PeopleManager.Data;
using PeopleManager.Data.Interfaces;
using PeopleManager.Data.Model;
using PeopleManager.DataManipulation;
using PeopleManager.DataManipulation.Interfaces;
using PeopleManager.Server.WCFServer.Interfaces;

namespace PeopleManager.Server.WCFServer.Services
{
    /// <summary>
    /// This class can be used as a service to expose a REST infrastructure for PeopleManager's people.
    /// </summary>
    public class PeopleManagerService : IPeopleManagerService, IDisposable
    {
        private readonly IItemRepository<Person> repository;
        private readonly IPeopleDataContext peopleDataContext;

        /// <summary>
        /// The constructor initializes an instance of this class.
        /// </summary>
        public PeopleManagerService()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["peopleManagerEntities"].ConnectionString;
            this.peopleDataContext = new PeopleDataContext(connectionString);
            this.repository = new PersonRepository(this.peopleDataContext);
        }

        /// <summary>
        /// This method adds a person to the server side repository.
        /// </summary>
        /// <param name="person">The person to add</param>
        public void PostPerson(Person person)
        {
            person = repository.Add(person);
        }

        /// <summary>
        /// This method reads all people from the server side repository.
        /// </summary>
        /// <returns>An array of people in the repository</returns>
        public Person[] GetAll()
        {
            var databaseProxyObjects = repository.GetAll().ToArray();
            var result = GetPocosFromDatabaseProxies(databaseProxyObjects);
            return result;
        }

        /// <summary>
        /// This method searches for a single person with a given key in the server side repository.
        /// </summary>
        /// <param name="id">The key of the person to be found</param>
        /// <returns>The matching person</returns>
        public Person Get(string id)
        {
            var person = repository.Get(Guid.Parse(id));
            var result = GetPocoFromDatabaseProxy(person);
            return result;
        }

        /// <summary>
        /// This method updates a person with its properties in the server side repository.
        /// </summary>
        /// <param name="id">The id of the person to update</param>
        /// <param name="person">The person to update</param>
        public void PutPerson(string id, Person person)
        {
            person.Id = Guid.Parse(id);
            repository.Update(person);
        }

        /// <summary>
        /// This method deletes a person from the server side repository.
        /// </summary>
        /// <param name="id">The id of the person to delete</param>
        public void DeletePerson(string id)
        {
            var person = repository.Get(Guid.Parse(id));
            repository.Remove(person);
        }

        private static T[] GetPocosFromDatabaseProxies<T>(T[] databaseProxyObjects)
        {
            var jsonArray = JArray.FromObject(databaseProxyObjects);
            var result = jsonArray.ToObject<T[]>();
            return result;
        }

        private static T GetPocoFromDatabaseProxy<T>(T databaseProxyObject)
        {
            var jsonArray = JObject.FromObject(databaseProxyObject);
            var result = jsonArray.ToObject<T>();
            return result;
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.repository?.Dispose();
                    this.peopleDataContext?.Dispose();
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
