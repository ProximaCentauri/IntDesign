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
        [ForeignKey("Customer")]
        public int Id { get; set; }      
        public virtual Customer Customer { get; set; }
    }
}
