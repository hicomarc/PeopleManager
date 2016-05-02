using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeopleManager.Data.Model;
using PeopleManager.DataAccess.Interfaces;
using RestSharp;

namespace PeopleManager.DataAccess.DataClients
{
    /// <summary>
    /// This class implements a WebApi2 service data client for PeopleManager.
    /// </summary>
    public class WebApi2DataClient : RestClientBase
    {
        /// <summary>
        /// This constructor initializes the instance with the url to the WebApi2 REST service
        /// </summary>
        /// <param name="url">The url to the WebApi2 service</param>
        public WebApi2DataClient(string url) : base(url) { }

        /// <summary>
        /// Returns the name of this data client.
        /// </summary>
        public override string Name { get { return nameof(WebApi2DataClient); } }
    }
}
