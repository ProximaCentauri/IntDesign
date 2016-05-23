using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Customer : Person
    {
        [Key]
        public int Id { get; set; }
                       
        public virtual Bank CustomerBank { get; set; }
        public virtual Company CustomerCompany { get; set; }        
        public virtual Spouse CustomerSpouse { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }
        public virtual ICollection<Utility> Utilities { get; set; }

        public static explicit operator Customer(DbSet<Customer> v)
        {
            throw new NotImplementedException();
        }

        public Customer()
        {            
            Dependents = new List<Dependent>();
            Utilities = new List<Utility>();
        }       
    }
}
