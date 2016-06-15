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
                new UtilityBillType() { Id = 1, Name = "Utilities (Power & Water)",
                                        UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 1, Name = "VECO"},
                                            new UtilityCompany {Id = 2, Name = "MECO"},
                                            new UtilityCompany {Id = 3, Name = "MCWD"}
                                        }
                },
                new UtilityBillType() { Id = 2, Name = "Broadband",
                                        UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 1, Name = "Bayan Broadband"},
                                            new UtilityCompany {Id = 2, Name = "SKY Broadband"},
                                            new UtilityCompany {Id = 3, Name = "Smart Broadband"},
                                            new UtilityCompany {Id = 4, Name = "Globe Broadband"}
                                        }
                },
                new UtilityBillType() { Id = 3, Name = "Cable Channel Provider",
                                        UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 1, Name = "Cablelink"},
                                            new UtilityCompany {Id = 2, Name = "Cignal TV"},
                                            new UtilityCompany {Id = 3, Name = "Sky Cable"}
                                        }
                },
                new UtilityBillType() { Id = 4, Name = "Insurance",
                                        UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 1, Name = "Ayala Life", BillTypeId = 4},
                                            new UtilityCompany {Id = 2, Name = "Philam Life"},
                                            new UtilityCompany {Id = 3, Name = "Pru Life U.K."},
                                            new UtilityCompany {Id = 4, Name = "Prudential Life"},
                                            new UtilityCompany {Id = 5, Name = "Sun Life of Canada (Phils.), Inc."}
                                        } },
                new UtilityBillType() { Id = 5, Name = "Telecom",
                                        UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 1, Name = "ABS-CBN Mobile"},
                                            new UtilityCompany {Id = 2, Name = "Bayantel"},
                                            new UtilityCompany {Id = 3, Name = "PLDT"},
                                            new UtilityCompany {Id = 4, Name = "Smart Communications Inc."},
                                            new UtilityCompany {Id = 5, Name = "Sun Broadbank (Postpaid)"},
                                            new UtilityCompany {Id = 6, Name = "Sun Cellular (Postpaid)"},
                                            new UtilityCompany {Id = 7, Name = "Globe Telecom"}
                                        }
                });            
         }
    }
}
