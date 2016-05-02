using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Server.WCFServer.Services;

namespace PeopleManager.Server.ConsoleApplication
{
    /// <summary>
    /// This class is just a self-hosted WCF service to make the service available.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry point of the program.
        /// </summary>
        /// <param name="args"></param>
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
