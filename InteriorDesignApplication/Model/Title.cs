using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Title
    {
        [Key]
        [ForeignKey("Customer")]
        public int Id { get; set; }
        public virtual Customer Customer { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ReleasedDate { get; set; }
        public string ScannedTitle { get; set; }
        
        public double UnitCost { get; set; }
        public double TotalPayment { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        
        public Title()
        {
            Payments = new List<Payment>();
        }

    }
}
