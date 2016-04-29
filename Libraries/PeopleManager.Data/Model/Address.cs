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
        public string Street { get; set; }

        [Required]
        public int Zip { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        //[Required]
        //public virtual Guid PersonID { get; set; }

        //[Required]
        //public virtual Person Person { get; set; }
    }
}
