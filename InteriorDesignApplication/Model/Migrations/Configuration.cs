namespace Model.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Model.ManagerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Model.ManagerDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //     
            context.UtilityBillTypes.AddOrUpdate(x => x.Id,
                new UtilityBillType()
                {
                    Id = 990,
                    Name = "Utilities (Power & Water)",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany { Id = 990, Name = "VECO"},
                                            new UtilityCompany { Id = 991, Name = "MECO"},
                                            new UtilityCompany { Id = 992, Name = "MCWD"}
                                        }
                },
                new UtilityBillType()
                {
                    Id = 991,
                    Name = "Broadband",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 990, Name = "Bayan Broadband"},
                                            new UtilityCompany {Id = 991, Name = "SKY Broadband"},
                                            new UtilityCompany {Id = 992, Name = "Smart Broadband"},
                                            new UtilityCompany {Id = 993, Name = "Globe Broadband"}
                                        }
                },
                new UtilityBillType()
                {
                    Id = 992,
                    Name = "Cable Channel Provider",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 990, Name = "Cablelink"},
                                            new UtilityCompany {Id = 991, Name = "Cignal TV"},
                                            new UtilityCompany {Id = 992, Name = "Sky Cable"}
                                        }
                },
                new UtilityBillType()
                {
                    Id = 993,
                    Name = "Insurance",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 990, Name = "Ayala Life"},
                                            new UtilityCompany {Id = 991, Name = "Philam Life"},
                                            new UtilityCompany {Id = 992, Name = "Pru Life U.K."},
                                            new UtilityCompany {Id = 993, Name = "Prudential Life"},
                                            new UtilityCompany {Id = 994, Name = "Sun Life of Canada (Phils.), Inc."}
                                        }
                },
                new UtilityBillType()
                {
                    Id = 994,
                    Name = "Telecom",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 990, Name = "ABS-CBN Mobile"},
                                            new UtilityCompany {Id = 991, Name = "Bayantel"},
                                            new UtilityCompany {Id = 992, Name = "PLDT"},
                                            new UtilityCompany {Id = 993, Name = "Smart Communications Inc."},
                                            new UtilityCompany {Id = 994, Name = "Sun Broadbank (Postpaid)"},
                                            new UtilityCompany {Id = 995, Name = "Sun Cellular (Postpaid)"},
                                            new UtilityCompany {Id = 996, Name = "Globe Telecom"}
                                        }
                });                        
         }
    }
}
