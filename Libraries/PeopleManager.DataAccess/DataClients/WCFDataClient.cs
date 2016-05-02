using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.DataAccess.DataClients
{
    /// <summary>
    /// This class implements a WCF service data client for PeopleManager.
    /// </summary>
    public class WCFDataClient : RestClientBase
    {
        /// <summary>
        /// This constructor initializes the instance with the url to the WCF REST service
        /// </summary>
        /// <param name="url">The url to the WCF service</param>
        public WCFDataClient(string url) : base(url) { }

        /// <summary>
        /// Returns the name of this data client.
        /// </summary>
        public override string Name { get { return nameof(WCFDataClient); } }
    }
}
