using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Data.Model
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public virtual string Street { get; set; }

        [Required]
        public virtual int Zip { get; set; }

        [Required]
        public virtual string City { get; set; }

        [Required]
        public virtual string State { get; set; }
    }
}
