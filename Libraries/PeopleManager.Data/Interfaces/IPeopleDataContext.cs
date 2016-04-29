using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Data.Model;

namespace PeopleManager.Data.Interfaces
{
    public interface IPeopleDataContext : IDisposable
    {
        DbSet<Address> Addresses { get; set; }

        DbSet<Person> People { get; set; }
    }
}
