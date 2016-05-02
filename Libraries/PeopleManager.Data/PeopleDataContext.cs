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
    /// <summary>
    /// This class represents the data context for PeopleManager. In here there are all entity sets this context deals with.
    /// </summary>
    public class PeopleDataContext : DbContext, IPeopleDataContext
    {
        /// <summary>
        /// Holds a list of addresses of all people
        /// </summary>
        public virtual DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Holds a list of all people
        /// </summary>
        public virtual DbSet<Person> People { get; set; }

        /// <summary>
        /// This constructor initializes a new data context using a given connectionstring
        /// </summary>
        /// <param name="connectionString">The connectionstring to use to connect to the database.</param>
        public PeopleDataContext(string connectionString) : base(connectionString) { }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
