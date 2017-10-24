namespace SolutionName.DataLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SolutionName.DataLayer.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SolutionName.DataLayer.SalesContext context)
        {
            context.SalesOrders.AddOrUpdate(
                so => so.CustomerName,
                new Model.SalesOrder { CustomerName = "Adam", PONumber = "123" },
                new Model.SalesOrder { CustomerName = "Michael" },
                new Model.SalesOrder { CustomerName = "David", PONumber = "15232" }
                );
        }
    }
}
