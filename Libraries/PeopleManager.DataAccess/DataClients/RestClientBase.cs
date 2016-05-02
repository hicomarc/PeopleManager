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
    /// <summary>
    /// This abstract class has the responsibility of dealing with a rest service.
    /// </summary>
    public abstract class RestClientBase : IDataClient
    {
        private bool disposedValue = false;
        private readonly RestClient client;

        /// <summary>
        /// The name of the data client
        /// </summary>
        public abstract string Name { get; }

        protected RestClientBase(string url)
        {
            this.client = new RestClient(url);
        }

        /// <summary>
        /// This method adds a new person via a REST service call.
        /// </summary>
        /// <param name="person">The person's data with address(es) to add</param>
        /// <returns>The added person with updated ids of person and address(es)</returns>
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

        /// <summary>
        /// This method reads all people's data including its addresses from a REST service.
        /// </summary>
        /// <returns>The list of people that are returned by the service.</returns>
        public List<Person> GetPeopleWithAddresses()
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

        /// <summary>
        /// This method updates a single person and adds, removes or updates the attached addresses accordingly via a REST service call.
        /// </summary>
        /// <param name="person">The person to update</param>
        /// <returns>True if the operation succeeds.</returns>
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

        /// <summary>
        /// This method removes a person with all its addresses via a REST service call.
        /// </summary>
        /// <param name="person">The person to remove</param>
        public void RemovePerson(Person person)
        {
            var request = new RestRequest("people/" + person.Id, Method.DELETE);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                this.HandleException(response.Content);
            }
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


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Nothing to dispose
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
