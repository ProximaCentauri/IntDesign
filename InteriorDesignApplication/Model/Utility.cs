using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Utility
    {
        [Key]
        public int Id { get; set; }

        public virtual UtilityCategory Category { get; set; }
        public virtual UtilitySubCategory SubCategory { get; set; }

        public string AccountName { get; set; }
        public string AccountId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? CutoffDate { get; set; }

        public string BillStatement { get; set; }
        public string Receipt { get; set; }
        public string Status { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
