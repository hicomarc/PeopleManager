using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Data.Model;

namespace PeopleManager.DataAccess.Interfaces
{
    /// <summary>
    /// This interface describes a data client. It has methods for manipulating PeopleManager's data.
    /// </summary>
    public interface IDataClient : IDisposable
    {
        /// <summary>
        /// The name of the data client
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The implementation of this method should add a new person.
        /// </summary>
        /// <param name="person">The person data with address(es) to add</param>
        /// <returns>The added person with updated ids of person and address(es)</returns>
        Person AddPerson(Person person);

        /// <summary>
        /// The implementation of this method should read all people with its addresses from the data source.
        /// </summary>
        /// <returns>The list of people that are returned by the data source.</returns>
        List<Person> GetPeopleWithAddresses();

        /// <summary>
        /// The implementation of this method should update a single person and add, remove or update the attached addresses accordingly.
        /// </summary>
        /// <param name="person">The person to update</param>
        /// <returns>True if the operation succeeds.</returns>
        bool UpdatePerson(Person person);

        /// <summary>
        /// The implementation of this method should remove a person with all its addresses.
        /// </summary>
        /// <param name="person">The person to remove</param>
        void RemovePerson(Person person);
    }
}
