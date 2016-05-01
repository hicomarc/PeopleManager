using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PeopleManager.Data.Model;
using PeopleManager.DataAccess.Interfaces;
using RestSharp;

namespace PeopleManager.DataAccess.DataClients
{
    public abstract class RestClientBase : IDataClient
    {
        private readonly RestClient client;

        protected RestClientBase(string url)
        {
            this.client = new RestClient(url);
        }
        
        public abstract string Name { get; }

        public Person AddPerson(Person person)
        {
            var request = new RestRequest("people", Method.POST);
            Person personToSend = GetPlainPersonObject(person);
            request.AddJsonBody(personToSend);
            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                this.HandleException(response.Content);
            }
            var personJObject = JObject.Parse(response.Content);
            var returnedPerson = personJObject.ToObject<Person>();

            return returnedPerson;
        }

        private void HandleException(string content)
        {
            throw new Exception(content);
        }

        private Person GetPlainPersonObject(Person person)
        {
            var result = new Person
            {
                Addresses = new List<Address>(person.Addresses.Select(p => new Address { City = p.City, State = p.State, Street = p.Street, Zip = p.Zip })),
                Age = person.Age,
                FirstName = person.FirstName,
                LastName = person.LastName
            };

            return result;
        }

        public List<Person> GetPeople()
        {
            var request = new RestRequest("people", Method.GET);
            var response = client.Execute(request);
            var personJArray = JArray.Parse(response.Content);
            var people = personJArray.ToObject<List<Person>>();
            foreach (var person in people)
            {
                person.Addresses = new List<Address>(person.Addresses);
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                this.HandleException(response.Content);
            }

            return people;
        }

        public void RemovePerson(Person person)
        {
            var request = new RestRequest("people/" + person.Id, Method.DELETE);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                this.HandleException(response.Content);
            }
        }

        public bool UpdatePerson(Person person)
        {
            var request = new RestRequest("people/" + person.Id, Method.PUT);
            Person personToSend = GetPlainPersonObject(person);
            request.AddJsonBody(personToSend);
            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                this.HandleException(response.Content);
            }

            return true;
        }
    }
}
