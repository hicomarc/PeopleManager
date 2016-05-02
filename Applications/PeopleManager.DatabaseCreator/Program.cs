using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Data;
using PeopleManager.Data.Model;

namespace PeopleManager.DatabaseCreator
{
    /// <summary>
    /// This class is here to create the database and fill it with some test data
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry point of this assembly
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["peopleManagerEntities"].ConnectionString;
                using (var dataContext = new PeopleDataContext(connectionString))
                {
                    dataContext.Database.CreateIfNotExists();

                    dataContext.People.AddRange(new List<Person> {
                        new Person
                        {
                            FirstName = "Marc",
                            LastName = "Plöchl",
                            Age = 31,
                            Addresses = new List<Address> {
                                new Address
                                {
                                    Street = "Hotterweg 20",
                                    Zip = 7023,
                                    City = "Zemendorf",
                                    State = "Austria"
                                },
                                new Address
                                {
                                    Street = "Hauptstrasse 1a",
                                    Zip = 7023,
                                    City = "Zemendorf",
                                    State = "Austria"
                                },
                            }
                        },
                        new Person
                        {
                            FirstName = "Angelika",
                            LastName = "Plöchl",
                            Age = 31,
                            Addresses = new List<Address> {
                                new Address
                                {
                                    Street = "Hotterweg 20",
                                    Zip = 7023,
                                    City = "Zemendorf",
                                    State = "Austria"
                                },
                                new Address
                                {
                                    Street = "Hauptstrasse 1a",
                                    Zip = 7023,
                                    City = "Zemendorf",
                                    State = "Austria"
                                },
                            }
                        },
                        new Person
                        {
                            FirstName = "Alexandra",
                            LastName = "Freiberger",
                            Age = 45,
                            Addresses = new List<Address> {
                                new Address
                                {
                                    Street = "Mattersburgerstrasse 30",
                                    Zip = 7202,
                                    City = "Bad Sauerbrunn",
                                    State = "Austria"
                                },
                                new Address
                                {
                                    Street = "Weinberggasse 5b",
                                    Zip = 7202,
                                    City = "Bad Sauerbrunn",
                                    State = "Austria"
                                },
                            }
                        },
                        new Person
                        {
                            FirstName = "Andrea",
                            LastName = "Fertl",
                            Age = 50,
                            Addresses = new List<Address> {
                                new Address
                                {
                                    Street = "Martinsgasse 4",
                                    Zip = 7203,
                                    City = "Wiesen",
                                    State = "Austria"
                                },
                            }
                        },
                    });

                    dataContext.SaveChanges();

                    Console.WriteLine("Database created!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"An error has occured on creating the database:
{ex.ToString()}");
                Console.ReadLine();
            }
        }
    }
}
