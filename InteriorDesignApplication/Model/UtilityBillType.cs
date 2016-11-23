using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UtilityBillType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UtilityCompany> UtilityCompanies { get; set; }

        public UtilityBillType()
        {
            UtilityCompanies = new List<UtilityCompany>();            
        } 
    }
}
