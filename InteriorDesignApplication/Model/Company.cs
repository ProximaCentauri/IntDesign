using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Company
    {
        [Key, ForeignKey("Customer")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string OptionalInformation { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
