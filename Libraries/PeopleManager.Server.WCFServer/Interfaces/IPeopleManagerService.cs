using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PeopleManager.Data.Model;

namespace PeopleManager.Server.WCFServer.Interfaces
{
    /// <summary>
    /// This interface describes a class that can serve as a REST service for people in PeopleManager.
    /// </summary>
    [ServiceContract]
    public interface IPeopleManagerService
    {
        /// <summary>
        /// The implementation of this method should add a person to a server side repository.
        /// </summary>
        /// <param name="person">The person to add</param>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/people/")]
        void PostPerson(Person person);

        /// <summary>
        /// The implementation of this method should return all people from the server side repository.
        /// </summary>
        /// <returns>The array of people found in the data source</returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, Method = "GET", UriTemplate = "/people/")]
        Person[] GetAll();

        /// <summary>
        /// The implementation of this method should return a single person based on a given key from the server side repository.
        /// </summary>
        /// <param name="id">The id of the person to be searched</param>
        /// <returns>The matching person</returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, Method = "GET", UriTemplate = "/people/{id}")]
        Person Get(string id);

        /// <summary>
        /// The implementation of this method should update a given person in the server side repository.
        /// </summary>
        /// <param name="id">The id of the person to update</param>
        /// <param name="person">The person to update</param>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "PUT", UriTemplate = "/people/{id}")]
        void PutPerson(string id, Person person);

        /// <summary>
        /// The implementation of this method should delete a person from the server side repository.
        /// </summary>
        /// <param name="id">The id of the person to delete</param>
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/people/{id}")]
        void DeletePerson(string id);
    }
}
