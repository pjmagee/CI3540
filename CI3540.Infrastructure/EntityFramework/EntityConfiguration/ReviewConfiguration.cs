using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
        {
            ToTable("Review");
            HasKey(r => r.Id);

            HasRequired(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .WillCascadeOnDelete(true);

            HasRequired(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .WillCascadeOnDelete(true);

            Property(r => r.Comment);
            Property(r => r.Created);
            Property(r => r.Approved);
            Property(r => r.Rating);
        }
    }
}