using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using CI3540.Core.Entities;

namespace CI3540.Infrastructure.EntityFramework.EntityConfiguration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        // http://stackoverflow.com/questions/6383576/entity-framework-4-1-code-first-self-referencing-one-to-many-and-many-to-many-as
        // http://stackoverflow.com/questions/3634996/entity-framework-how-do-i-map-a-self-referencial-foreign-key-eg-category-has

        public CategoryConfiguration()
        {
            ToTable("Category");
            HasKey(c => c.Id);

            HasOptional(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .WillCascadeOnDelete(false);

            HasMany(c => c.Products)
                .WithMany(p => p.Categories)
                .Map(ProductCategories);

            Property(c => c.Name);
            Property(c => c.Description);
        }

        private void ProductCategories(ManyToManyAssociationMappingConfiguration config)
        {
            config.ToTable("ProductCategories");
            config.MapLeftKey("ProductId");
            config.MapRightKey("CategoryId");
        }
    }
}