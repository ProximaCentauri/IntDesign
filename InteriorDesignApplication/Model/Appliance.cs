using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Helpers;

namespace Model
{
    public class Appliance
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string ModelNumber { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? WarrantyEndDate { get; set; }

        public string Receipt { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
