using System.Data.Entity.ModelConfiguration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(u => u.Id);
        }
    }
}