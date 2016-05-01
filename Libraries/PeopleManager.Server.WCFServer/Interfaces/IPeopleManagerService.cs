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
    [ServiceContract]
    public interface IPeopleManagerService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, Method = "GET", UriTemplate = "/people/")]
        Person[] GetAll();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, Method = "GET", UriTemplate = "/people/{id}")]
        Person Get(string id);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "/people/")]
        void PostPerson(Person person);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "PUT", UriTemplate = "/people/{id}")]
        void PutPerson(string id, Person person);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/people/{id}")]
        void DeletePerson(string id);
    }
}
