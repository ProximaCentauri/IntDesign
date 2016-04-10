using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public class ManagerDBContext : DbContext
    {
        public ManagerDBContext() 
            : base("SetupConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ManagerDBContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ManagerDBContext>());

            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
    }
}
