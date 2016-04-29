using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PeopleManager.Data.Model;
using PeopleManager.DataManipulation;
using PeopleManager.DataManipulation.Interfaces;

namespace PeopleManager.WebService.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly IItemRepository<Person> repository = new PersonRepository();

        public IEnumerable<Person> GetAll()
        {
            return repository.GetAll();
        }

        public Person Get(Guid id)
        {
            var person = repository.Get(id);
            if (person == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return person;
        }

        public HttpResponseMessage PostPerson(Person person)
        {
            person = repository.Add(person);
            var response = Request.CreateResponse(HttpStatusCode.Created, person);

            string uri = Url.Link("DefaultApi", new { id = person.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutPerson(Guid id, Person person)
        {
            person.Id = id;
            if (!repository.Update(person))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeletePerson(Guid id)
        {
            var person = repository.Get(id);
            if (person == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(person);
        }
    }
}
