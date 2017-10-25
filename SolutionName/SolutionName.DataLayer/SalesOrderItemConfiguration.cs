using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.DataLayer
{
    public class SalesOrderItemConfiguration : EntityTypeConfiguration<SalesOrderItem>
    {
        public SalesOrderItemConfiguration()
        {
            Property(soi => soi.ProductCode).HasMaxLength(15).IsRequired();
            Property(soi => soi.Quantity).IsRequired();
            Property(soi => soi.UnitPrice).IsRequired();
            Ignore(soi => soi.ObjectState);
        }
    }
}
