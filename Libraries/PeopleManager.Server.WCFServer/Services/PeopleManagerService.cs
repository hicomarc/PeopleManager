using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PeopleManager.Data.Model;
using PeopleManager.DataManipulation;
using PeopleManager.DataManipulation.Interfaces;
using PeopleManager.Server.WCFServer.Interfaces;

namespace PeopleManager.Server.WCFServer.Services
{
    public class PeopleManagerService : IPeopleManagerService
    {
        private readonly IItemRepository<Person> repository = new PersonRepository();

        public Person[] GetAll()
        {
            var databaseProxyObjects = repository.GetAll().ToArray();
            var result = GetPocosFromDatabaseProxies(databaseProxyObjects);
            return result;
        }

        public Person Get(string id)
        {
            var person = repository.Get(Guid.Parse(id));
            var result = GetPocoFromDatabaseProxy(person);
            return result;
        }

        public void PostPerson(Person person)
        {
            person = repository.Add(person);
        }

        public void PutPerson(string id, Person person)
        {
            person.Id = Guid.Parse(id);
            repository.Update(person);
        }

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
    }
}
