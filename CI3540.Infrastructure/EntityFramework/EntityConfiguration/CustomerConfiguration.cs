using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            HasKey(c => c.Id);

            HasOptional(c => c.Cart)
                .WithRequired(cart => cart.Customer)
                .WillCascadeOnDelete(false);

            HasMany(c => c.Orders)
                .WithRequired(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .WillCascadeOnDelete(true);

            HasMany(c => c.Reviews)
                .WithRequired(r => r.Customer)
                .WillCascadeOnDelete(true);

            Property(c => c.Forename);
            Property(c => c.Surname);
            Property(c => c.Email);
            Property(c => c.DateCreated);
            Property(c => c.DateModified);
            Property(c => c.PhoneNumber);            
        }
    }
}