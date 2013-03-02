using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            ToTable("Address");
            HasKey(a => a.Id);

            HasRequired(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CustomerId)
                .WillCascadeOnDelete(true);

            Property(a => a.AddressLine1);
            Property(a => a.AddressLine2);
            Property(a => a.City);
            Property(a => a.County);
            Property(a => a.PostCode);
        }
    }
}