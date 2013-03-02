using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    /// <summary>
    /// The Order entity configuration
    /// </summary>
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Order");
            HasKey(o => o.Id);

            HasMany(o => o.OrderLines)
                .WithOptional(ol => ol.Order)
                .HasForeignKey(ol => ol.OrderId)
                .WillCascadeOnDelete(true);
            
            HasOptional(o => o.Employee)
                .WithMany()
                .HasForeignKey(o => o.EmployeeId)
                .WillCascadeOnDelete(false);

            HasRequired(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .WillCascadeOnDelete(true);

            
            Property(o => o.DateCreated);
            Property(o => o.Total);
            Property(o => o.Status);
        }
    }
}