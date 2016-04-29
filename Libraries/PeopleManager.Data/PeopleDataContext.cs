using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Data.Interfaces;
using PeopleManager.Data.Model;

namespace PeopleManager.Data
{
    public class PeopleDataContext : DbContext, IPeopleDataContext
    {
        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public PeopleDataContext(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
