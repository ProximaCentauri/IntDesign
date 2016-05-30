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
    public class Utility
    {
        [Key]
        public int Id { get; set; }

        public virtual UtilityBillType BillType { get; set; }
        public virtual UtilityCompany UtilityCompany { get; set; }

        public string AccountName { get; set; }
        public string AccountId { get; set; }

        [Column(TypeName = "Date")]       
        public DateTime? CutoffDate { get; set; }       

        public string BillStatement { get; set; }
        public string Receipt { get; set; }


        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [NotMapped]
        public string Status
        {
            get
            {
                return UtilityHelper.GetUtilityStatusText(CutoffDate, !String.IsNullOrEmpty(Receipt));
            }
        }

        [NotMapped]
        public int DaysDue
        {
            get
            {
                return UtilityHelper.GetUtilityDaysDue(CutoffDate);
            }
        }
    
    }
}
