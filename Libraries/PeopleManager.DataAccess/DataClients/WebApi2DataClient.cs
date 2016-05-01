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
    public class WebApi2DataClient : RestClientBase
    {
        public WebApi2DataClient(string url) : base(url) { }

        public override string Name
        {
            get
            {
                return nameof(WebApi2DataClient);
            }
        }
    }
}
