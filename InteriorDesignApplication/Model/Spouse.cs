using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Spouse : Person
    {
        [Key]
        public int Id { get; set; }

        //public int CustomerId { get; set; }
        //[ForeignKey("CustomerId")]
        //public virtual Customer Customer { get; set; }
        [Required]
        public virtual Customer customer { get; set; }
    }
}
