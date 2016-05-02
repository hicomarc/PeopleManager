using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Data.Model
{
    /// <summary>
    /// This class represents address data of people
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The unique identification key of this address
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// The street where the person lives
        /// </summary>
        [Required]
        public virtual string Street { get; set; }

        /// <summary>
        /// The zip code where the person lives
        /// </summary>
        [Required]
        public virtual int Zip { get; set; }

        /// <summary>
        /// The city where the person lives
        /// </summary>
        [Required]
        public virtual string City { get; set; }

        /// <summary>
        /// The state where the person lives
        /// </summary>
        [Required]
        public virtual string State { get; set; }
    }
}
