using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public string SwiftCode { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]        
        public virtual Customer Customer { get; set; }
    }
}
