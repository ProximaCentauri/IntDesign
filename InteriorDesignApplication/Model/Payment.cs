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
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? PaymentDate { get; set; } 
        public string ChequeNumber { get; set; }
        public double Amount { get; set; }
        public string Cheque { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

    }
}
