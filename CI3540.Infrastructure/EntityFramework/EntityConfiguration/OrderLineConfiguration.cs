using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class OrderLineConfiguration : EntityTypeConfiguration<OrderLine>
    {
        public OrderLineConfiguration()
        {
            ToTable("OrderLine");
            HasKey(ol => ol.Id);

            HasRequired(ol => ol.Product)
                .WithMany()
                .HasForeignKey(ol => ol.ProductId)
                .WillCascadeOnDelete(true);

            HasOptional(ol => ol.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(ol => ol.OrderId)
                .WillCascadeOnDelete(true);

            HasOptional(ol => ol.Cart)
                .WithMany(c => c.OrderLines)
                .HasForeignKey(ol => ol.CartId)
                .WillCascadeOnDelete(true);

            Property(ol => ol.Status);
            Property(ol => ol.Tracking);
        }
    }
}