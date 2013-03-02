using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Product");
            HasKey(p => p.Id);

            //HasMany(p => p.Categories).WithMany(c => c.Products);

            HasMany(p => p.Reviews)
                .WithRequired(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .WillCascadeOnDelete(true);

            HasMany(p => p.ProductImages)
                .WithRequired(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .WillCascadeOnDelete(true);
            
            Property(p => p.Stock);
            Property(p => p.Name);
            Property(p => p.Price);
            Property(p => p.Description);
            Property(p => p.Modified);
            Property(p => p.Created);

        }
    }
}