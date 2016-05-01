using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.DataAccess.DataClients
{
    public class WCFDataClient : RestClientBase
    {
        public WCFDataClient(string url) : base(url) { }

        public override string Name
        {
            get
            {
                return nameof(WCFDataClient);
            }
        }
    }
}
