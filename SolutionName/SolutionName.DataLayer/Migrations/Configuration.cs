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
                new Model.SalesOrder
                {
                    CustomerName = "Adam", PONumber = "123", SalesOrderItems =
                    {
                        new Model.SalesOrderItem { ProductCode = "Car", Quantity=1, UnitPrice = 2.13m},
                        new Model.SalesOrderItem { ProductCode = "dog", Quantity=11, UnitPrice = 3.13m},
                        new Model.SalesOrderItem { ProductCode = "Pants", Quantity=31, UnitPrice = 12.13m},
                        new Model.SalesOrderItem { ProductCode = "Goship", Quantity=21, UnitPrice = 32.13m}
                    }
                },
                new Model.SalesOrder { CustomerName = "Michael" },
                new Model.SalesOrder { CustomerName = "David", PONumber = "15232" }
                );
        }
    }
}
