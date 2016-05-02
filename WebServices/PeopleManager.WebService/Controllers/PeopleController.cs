using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PeopleManager.Data;
using PeopleManager.Data.Interfaces;
using PeopleManager.Data.Model;
using PeopleManager.DataManipulation;
using PeopleManager.DataManipulation.Interfaces;

namespace PeopleManager.WebService.Controllers
{
    public class PeopleController : ApiController, IDisposable
    {
        private readonly IItemRepository<Person> repository;
        private readonly IPeopleDataContext peopleDataContext;

        /// <summary>
        /// The constructor initializes the instance of this class.
        /// </summary>
        public PeopleController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["peopleManagerEntities"].ConnectionString;
            this.peopleDataContext = new PeopleDataContext(connectionString);
            this.repository = new PersonRepository(this.peopleDataContext);
        }

        /// <summary>
        /// This method adds a person to the server side repository.
        /// </summary>
        /// <param name="person">The person to add</param>
        /// <returns>A response message with the new id of the person added</returns>
        public HttpResponseMessage PostPerson(Person person)
        {
            person = repository.Add(person);
            var response = Request.CreateResponse(HttpStatusCode.Created, person);

            string uri = Url.Link("DefaultApi", new { id = person.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// This method reads all people from the server side repository.
        /// </summary>
        /// <returns>An enumeration of people in the server side repository</returns>
        public IEnumerable<Person> GetAll()
        {
            return repository.GetAll();
        }

        /// <summary>
        /// This method searches for a person with a given key.
        /// </summary>
        /// <param name="id">The person's key to search for</param>
        /// <returns>The matching person</returns>
        public Person Get(Guid id)
        {
            var person = repository.Get(id);
            if (person == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return person;
        }

        /// <summary>
        /// This method updates a person with all its properties and addresses in the server side repository.
        /// </summary>
        /// <param name="id">The id of the person to update</param>
        /// <param name="person">The person-object to update</param>
        public void PutPerson(Guid id, Person person)
        {
            person.Id = id;
            if (!repository.Update(person))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// This method removes a person from the server side repository.
        /// </summary>
        /// <param name="id">The id of the person</param>
        public void DeletePerson(Guid id)
        {
            var person = repository.Get(id);
            if (person == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.repository?.Dispose();
                this.peopleDataContext?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
