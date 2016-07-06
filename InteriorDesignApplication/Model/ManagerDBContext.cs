using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;
using Model.Migrations;

namespace Model
{   
    public class ManagerDBContext : DbContext
    {
        public ManagerDBContext()
            : base("name=setupConnectionString")
        {           
            Database.SetInitializer<ManagerDBContext>(new MigrateDatabaseToLatestVersion<ManagerDBContext, Configuration>());           
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet Company { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet Spouse { get; set; }
        public DbSet<UtilityBillType> UtilityBillTypes { get; set; }
        public DbSet<UtilityCompany> UtilityCompanies { get; set; }
        public DbSet<Utility> Utilities { get; set; }
        public DbSet<Appliance> Appliances { get; set; }        
        public DbSet Title { get; set; }
        public DbSet FitOut { get; set; }       
        public DbSet Payments { get; set; }
    }
}
