﻿using System;
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
        public DbSet Bank { get; set; }
        public DbSet Spouse { get; set; }
        public DbSet<UtilityCategory> UtilityCategories { get; set; }
        public DbSet<UtilitySubCategory> UtilitySubCategories { get; set; }
        public DbSet<Utility> Utilities { get; set; }
    }
}
