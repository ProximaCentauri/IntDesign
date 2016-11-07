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

            SeedDefaultUtilityBillTypeAndCompany(context);
            SeedDefaultBank(context);
            SeedAppUser(context);
            
            
         }

        private void SeedDefaultUtilityBillTypeAndCompany(Model.ManagerDBContext context)
        {
            context.UtilityBillTypes.AddOrUpdate(x => x.Id,
                new UtilityBillType()
                {
                    Id = 1,
                    Name = "Utilities (Power & Water)",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany { Id = 1, Name = "VECO"},
                                            new UtilityCompany { Id = 2, Name = "MECO"},
                                            new UtilityCompany { Id = 3, Name = "MCWD"}
                                        }
                },
                new UtilityBillType()
                {
                    Id = 2,
                    Name = "Broadband",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 1, Name = "Bayan Broadband"},
                                            new UtilityCompany {Id = 2, Name = "SKY Broadband"},
                                            new UtilityCompany {Id = 3, Name = "Smart Broadband"},
                                            new UtilityCompany {Id = 4, Name = "Globe Broadband"}
                                        }
                },
                new UtilityBillType()
                {
                    Id = 3,
                    Name = "Cable Channel Provider",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 1, Name = "Cablelink"},
                                            new UtilityCompany {Id = 2, Name = "Cignal TV"},
                                            new UtilityCompany {Id = 3, Name = "Sky Cable"}
                                        }
                },
                new UtilityBillType()
                {
                    Id = 4,
                    Name = "Insurance",
                    UtilityCompanies = new List<UtilityCompany>()
                                        {
                                            new UtilityCompany {Id = 1, Name = "Ayala Life"},
                                            new UtilityCompany {Id = 2, Name = "Philam Life"},
                                            new UtilityCompany {Id = 3, Name = "Pru Life U.K."},
                                            new UtilityCompany {Id = 4, Name = "Prudential Life"},
                                            new UtilityCompany {Id = 5, Name = "Sun Life of Canada (Phils.), Inc."}
                                        }
                },
                new UtilityBillType()
                {
                    Id = 5,
                    Name = "Telecom",
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

        private void SeedDefaultBank(Model.ManagerDBContext context)
        {
            context.BankTypes.AddOrUpdate(x => x.Id,
                new BankType()
                {
                    Id = 1,
                    Name = "Allied Bank"
                },
                new BankType()
                {
                    Id = 2,
                    Name = "Asia United Bank"
                },
                new BankType()
                {
                    Id = 3,
                    Name = "Bank of Commerce"
                },
                new BankType()
                {
                    Id = 4,
                    Name = "BDO"
                },
                new BankType()
                {
                    Id = 5,
                    Name = "BPI"
                },
                new BankType()
                {
                    Id = 6,
                    Name = "BPI Family"
                },
                new BankType()
                 {
                     Id = 7,
                     Name = "China Bank"
                 },
                new BankType()
                {
                    Id = 8,
                    Name = "Citibank N.A"
                },
                new BankType()
                {
                    Id = 9,
                    Name = "East West Bank"
                },
                new BankType()
                 {
                     Id = 10,
                     Name = "HSBC"
                 },
                new BankType()
                {
                    Id = 11,
                    Name = "Land Bank"
                },
                new BankType()
                {
                    Id = 12,
                    Name = "Maybank"
                },
                new BankType()
                 {
                     Id = 13,
                     Name = "Metrobank"
                 },
                new BankType()
                {
                    Id = 14,
                    Name = "PNB"
                },
                new BankType()
                {
                    Id = 15,
                    Name = "PSBank"
                },
                new BankType()
                {
                    Id = 16,
                    Name = "RCBC"
                },
                new BankType()
                {
                    Id = 17,
                    Name = "Security Bank"
                },
                new BankType()
                {
                    Id = 18,
                    Name = "Standard Chartered"
                },
                new BankType()
                {
                    Id = 19,
                    Name = "UCPB"
                },
                new BankType()
                {
                    Id = 20,
                    Name = "Union Bank of the Philippines"
                });
        }

        private void SeedAppUser(Model.ManagerDBContext context)
        {
            context.AppUsers.AddOrUpdate(x => x.Id,
                new AppUser()
                {
                    Id = 1,
                    UserName = "Admin",
                    CurrentPassword = "jasper",
                    EmailAddress = "proximacentauriofficial@gmail.com"
                },
                new AppUser()
                {
                    Id = 2,
                    UserName = "Guest1"
                },
                new AppUser()
                {
                    Id = 3,
                    UserName = "Guest2"
                });
        }
    }
}
