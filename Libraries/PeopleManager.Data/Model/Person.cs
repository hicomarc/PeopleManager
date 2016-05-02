using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Data.Model
{
    /// <summary>
    /// This class represents a person with its properties
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The unique identification key of this person
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// The first name of this person
        /// </summary>
        [Required]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// The last name of this person
        /// </summary>
        [Required]
        public virtual string LastName { get; set; }

        /// <summary>
        /// The age of this person as of entry into the database
        /// </summary>
        [Required]
        public virtual int Age { get; set; }

        /// <summary>
        /// The list of addresses this person has
        /// </summary>
        [Required]
        public virtual List<Address> Addresses { get; set; }
    }
}
