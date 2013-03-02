using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class CartConfiguration : EntityTypeConfiguration<Cart>
    {
        public CartConfiguration()
        {
            ToTable("Cart");
            HasKey(c => c.Id);

            HasMany(c => c.OrderLines)
                .WithOptional(ol => ol.Cart)
                .HasForeignKey(ol => ol.CartId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.Customer)
                .WithOptional(c => c.Cart)
                .WillCascadeOnDelete(true);

            Property(c => c.Created);
            Property(c => c.Modified);
        }


    }
}
