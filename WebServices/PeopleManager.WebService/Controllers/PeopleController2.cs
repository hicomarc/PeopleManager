using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PeopleManager.Data;
using PeopleManager.Data.Model;
using PeopleManager.DataManipulation;

namespace PeopleManager.WebService.Controllers
{
    public class PeopleController2 : ApiController
    {
        private readonly PersonRepository personRepository;

        public PeopleController2()
        {
            this.personRepository = new PersonRepository();
        }

        // GET api/values
        public IEnumerable<Person> Get()
        {
            var result = personRepository.GetAll().ToList();
            return result;
        }

        // GET api/values/5
        public Person Get(Guid id)
        {
            var result = personRepository.Get(id);
            return result;
        }

        // POST api/values
        public Person Post([FromBody]Person item)
        {
            var result = personRepository.Add(item);
            return result;
        }

        // PUT api/values/5
        public Person Put([FromBody]Person item)
        {
            var result = personRepository.Update(item);
            return item;
        }

        // DELETE api/values/5
        public void Delete(Person item)
        {
            personRepository.Remove(item);
        }
    }
}
