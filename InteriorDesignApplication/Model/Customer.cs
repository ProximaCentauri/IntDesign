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
       
        public virtual IEnumerable<Dependent> Dependents { get; set; }

        public static explicit operator Customer(DbSet<Customer> v)
        {
            throw new NotImplementedException();
        }
    }
}
