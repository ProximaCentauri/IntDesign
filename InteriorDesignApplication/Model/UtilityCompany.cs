using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UtilityCompany
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int BillTypeId { get; set; }
        [ForeignKey("BillTypeId")]
        public virtual UtilityBillType BillType { get; set; }
    }
}
