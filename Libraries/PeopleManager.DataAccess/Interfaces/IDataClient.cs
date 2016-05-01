using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Data.Model;

namespace PeopleManager.DataAccess.Interfaces
{
    public interface IDataClient
    {
        string Name { get; }

        List<Person> GetPeople();

        bool UpdatePerson(Person person);

        Person AddPerson(Person person);

        void RemovePerson(Person person);
    }
}
