using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CI3540.Core.Entities;
using CI3540.Infrastructure.EntityFramework.EntityConfiguration;

namespace CI3540.Infrastructure.EntityFramework
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("DefaultConnection")
        {
            // http://stackoverflow.com/questions/3634996/entity-framework-how-do-i-map-a-self-referencial-foreign-key-eg-category-has

            Configuration.ProxyCreationEnabled = true; // Our Navigational Properties
            Configuration.LazyLoadingEnabled = true; // Lazy Loading of Collections, Faster performance
            Configuration.AutoDetectChangesEnabled = true; // Change Tracker for changes made on either side of the Association through Proxy Class
            Configuration.ValidateOnSaveEnabled = true; // Validate On Saving for Data Integrity
        }

        // These are all the entities which Entity Framework maps to a real Database entity
        // These are what I call in my service layer to get the entities from the database

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();      
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            ConfigureModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureModel(DbModelBuilder modelBuilder)
        {
            // With entity framework you can create configurations for your entity classes
            // and then from ModelBuilder you add these configurations which
            // define all the associations between the entites as well as data integrity 

            modelBuilder
                .Configurations
                .Add(new UserConfiguration())
                .Add(new EmployeeConfiguration())
                .Add(new CustomerConfiguration())
                .Add(new ProductConfiguration())
                .Add(new ProductImageConfiguration())
                .Add(new OrderConfiguration())
                .Add(new CategoryConfiguration())
                .Add(new ReviewConfiguration())
                .Add(new OrderLineConfiguration());
        }


    }
}
