using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employee");
            HasKey(e => e.Id);
            Property(e => e.EmployeeNumber);
            Property(e => e.Email);
            Property(e => e.Forename);
            Property(e => e.Surname);
            Property(e => e.DateCreated);
            Property(e => e.DateModified);
        }
    }
}
