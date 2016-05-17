namespace Model.Migrations
{
    using System;
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

            context.Customers.AddOrUpdate(
                c => c.LastName,
                new Customer { LastName = "Doe", FirstName = "Jane", MiddleName = "Test1" },
                new Customer { LastName = "Reynolds", FirstName = "Gray", MiddleName = "Test2" },
                new Customer
                {
                    LastName = "Wick",
                    FirstName = "John",
                    MiddleName = "Test3",
                    Status = "Married",
                    CustomerSpouse = new Spouse { LastName = "Wick", FirstName = "Mary" }
                });
         }
    }
}
