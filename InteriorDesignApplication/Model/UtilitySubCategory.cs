using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UtilitySubCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual UtilityCategory Category { get; set; }
    }
}
