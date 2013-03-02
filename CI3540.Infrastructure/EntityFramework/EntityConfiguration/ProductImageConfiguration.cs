using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class ProductImageConfiguration : EntityTypeConfiguration<ProductImage>
    {
        public ProductImageConfiguration()
        {
            ToTable("ProductImage");
            HasKey(pi => pi.Id);

            HasRequired(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .WillCascadeOnDelete(true);

            Property(pi => pi.Path);
            Property(pi => pi.Primary);
        }
    }
}