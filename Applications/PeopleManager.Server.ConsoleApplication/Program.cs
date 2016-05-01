using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Server.WCFServer.Services;

namespace PeopleManager.Server.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(PeopleManagerService)))
            {
                host.Open();

                Console.WriteLine("Service has started. Press <Enter> to quit.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
