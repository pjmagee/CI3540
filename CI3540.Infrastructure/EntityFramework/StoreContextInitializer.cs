using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using CI3540.Core.Entities;
using Faker;
using FizzWare.NBuilder;
using WebMatrix.WebData;
using Address = CI3540.Core.Entities.Address;

namespace CI3540.Infrastructure.EntityFramework
{
    public class StoreContextInitializer : CreateDatabaseIfNotExists<StoreContext>
    {

        // I used the built in WebSecurity in ASP.NET MVC 
        // Andadd two roles to the site for Administrators and Customers
        // WebSecurity creates its own tables and so you pass in the connection from the app.config file
        // which contains the connection string

        private static void InitializeWebSecurity()
        {
            // http://blog.longle.net/2012/09/25/seeding-users-and-roles-with-mvc4-simplemembershipprovider-simpleroleprovider-ef5-codefirst-and-custom-user-properties/

            if (WebSecurity.Initialized)
                return;

            WebSecurity.InitializeDatabaseConnection(
                connectionStringName: "DefaultConnection",
                userTableName: "User",
                userIdColumn: "Id",
                userNameColumn: "Email",
                autoCreateTables: true);
                
            Roles.CreateRole("Admin");
            Roles.CreateRole("Customer");
        }
        
        // SEED THE DATABASE WITH THIS FAKE DATA
        // I used a Library called Faker.Net and another library called Nbuilder
        // with these I was able to seet my database with inital products
        // and categories so that data pre-exists in the website

        protected override void Seed(StoreContext context)
        {
            InitializeWebSecurity();

            var generator = new RandomGenerator();

            // Inspired by
            // http://www.amazon.co.uk/gp/site-directory/ref=sa_menu_fullstore

            // Issues slaughtered with the help of
            // http://e10.in/blog/ef4-multiple-added-entities-may-have-the-same-primary-key
            // http://stackoverflow.com/questions/4438574/adding-list-of-objects-to-context-in-ef
            // http://nbuilder.org/Documentation/Lists

            var categories = CreateCategories(generator).ToList();
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var customers = CreateCustomers(generator, categories).ToList();
            var employees = CreateEmployees(generator).ToList();

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();
            customers.ForEach(AddCustomerToWebSecurity);

            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();
            employees.ForEach(AddEmployeeToWebSecurity);

            context.SaveChanges();
        }

        private static IEnumerable<Employee> CreateEmployees(RandomGenerator generator)
        {
            var employees = Builder<Employee>
                .CreateListOfSize(5)
                .All()
                .Do(e =>
                    {
                        string name = Name.FullName(NameFormats.Standard);
                        e.Forename = name.Split(' ').First();
                        e.Surname = name.Split(' ').Last();
                        e.Email = Internet.Email(name);
                        e.PhoneNumber = Phone.Number();
                        e.EmployeeNumber = generator.Guid().ToString();
                    })
                .Build();

            employees[0].Email = "admin";

            return employees;
        }

        private static IEnumerable<Category> CreateCategories(RandomGenerator generator)
        {
            var categories = Builder<Category>
                .CreateListOfSize(5)
                .TheFirst(1)
                .Do(c =>
                    {
                        c.Name = "Groceries";
                        c.Description = "Drinks, Foods, Health, Beauty, Household, Snacks, Vegetables, Butcher Selection, Finest Foods, Selections, Alcohol";
                        var names = c.Description.Split(',').ToList();
                        c.ParentId = null;
                        c.Parent = null;
                        c.Children = CreateChildren(c, names, generator).ToList();
                    })
                .TheNext(1)
                .Do(c =>
                    {
                        c.Name = "Apparel";
                        c.Description = "Clothes, Shoes, Accessories, Watches, Bags, Trainers, Hiker Gear, Ski Wear, Sport Wear, Designer Store";
                        var names = c.Description.Split(',').ToList();
                        c.ParentId = null;
                        c.Parent = null;
                        c.Children = CreateChildren(c, names, generator).ToList();
                    })
                .TheNext(1)
                .Do(c =>
                    {
                        c.Name = "Digital Media";
                        c.Description = "Music, Film, TV, Games, Blu-ray";
                        var names = c.Description.Split(',').ToList();
                        c.ParentId = null;
                        c.Parent = null;
                        c.Children = CreateChildren(c, names, generator).ToList();
                    })
                .TheNext(1)
                .Do(c =>
                    {
                        c.Name = "Electronics";
                        c.Description = "Home Cinema, Phones, TV Sets, Computers & Laptops, Music Players";
                        var names = c.Description.Split(',').ToList();
                        c.ParentId = null;
                        c.Parent = null;
                        c.Children = CreateChildren(c, names, generator).ToList();
                    })
                .TheLast(1)
                .Do(c =>
                    {
                        c.Name = "Sports & Outdoors";
                        c.Description = "Camping, Golf, Health, Exercise, Gym";
                        var names = c.Description.Split(',').ToList();
                        c.ParentId = null;
                        c.Parent = null;
                        c.Children = CreateChildren(c, names, generator).ToList();
                    })
                .Build();

            return categories;
        }

        private static IEnumerable<Customer> CreateCustomers(RandomGenerator generator, IList<Category> categories)
        {
            var customers = Builder<Customer>
                .CreateListOfSize(5)
                .All()
                .Do(c =>
                    {
                        string name = Name.FullName(NameFormats.Standard);
                        c.Forename = name.Split(' ').First();
                        c.Surname = name.Split(' ').Last();
                        c.Email = Internet.Email(name);
                        c.PhoneNumber = Phone.Number();
                        c.Cart = Builder<Cart>
                            .CreateNew()
                            .With(cart => cart.Customer = c)
                            .And(cart => cart.Created = DateTime.Now)
                            .And(cart => cart.Modified = DateTime.Now)
                            .And(cart => cart.OrderLines = Builder<OrderLine>.CreateListOfSize(5)
                                            .All()
                                            .Do(ol =>
                                            {
                                                ol.Cart = cart;
                                                ol.CartId = null;
                                                ol.Order = null;
                                                ol.OrderId = null;
                                                var parentCategory = Pick<Category>.RandomItemFrom(categories);
                                                var childCategory = Pick<Category>.RandomItemFrom(parentCategory.Children.ToList());
                                                var acceptableProducts = childCategory.Products.Where(product => product.Stock > 0).ToList();
                                                var randomProduct = Pick<Product>.RandomItemFrom(acceptableProducts);
                                                ol.Product = randomProduct;
                                                ol.ProductId = randomProduct.Id;
                                                ol.Quantity = SelectQuantity(ol, generator);
                                                ol.Status = Status.Pending;
                                                ol.Tracking = ol.Status == Status.Shipping ? generator.Guid().ToString() : null;
                                            })
                                            .Build())
                            .Build();

                        c.CartId = null;
                        c.Reviews = Builder<Review>
                            .CreateListOfSize(5)
                            .All()
                            .Do(r =>
                                {
                                    r.Customer = c;
                                    r.CustomerId = c.Id;
                                    r.Approved = true;
                                    r.Rating = generator.Next(1f, 5f);
                                    r.Comment = Lorem.Sentence(generator.Next(1, 3));
                                    var parent = Pick<Category>.RandomItemFrom(categories);
                                    var products = Pick<Category>.RandomItemFrom(parent.Children.ToList()).Products.ToList();
                                    var randomProduct = Pick<Product>.RandomItemFrom(products);
                                    r.Product = randomProduct;
                                    r.ProductId = randomProduct.Id;
                                })
                            .Build();
                        c.Addresses = Builder<Address>
                            .CreateListOfSize(3)
                            .All()
                            .Do(a =>
                                {
                                    a.Customer = c;
                                    a.CustomerId = c.Id;
                                    a.City = Faker.Address.City();
                                    a.PostCode = Faker.Address.UkPostCode().ToUpper();
                                    a.County = Faker.Address.UkCounty();
                                    a.AddressLine1 = Faker.Address.StreetAddress();
                                    a.AddressLine2 = Faker.Address.SecondaryAddress();
                                })
                            .Build();
                        c.Orders = Builder<Order>
                            .CreateListOfSize(5)
                            .All()
                            .Do(o =>
                                {
                                    o.Customer = c;
                                    o.CustomerId = c.Id;
                                    o.Employee = null;
                                    o.EmployeeId = null;
                                    o.DateCreated = generator.DateTime();
                                    o.BillingAddress = c.Addresses.First();
                                    o.BillingAddressId = c.Addresses.First().Id;
                                    o.ShippingAddress = c.Addresses.First();
                                    o.ShippingAddressId = c.Addresses.First().Id;
                                    o.OrderLines = Builder<OrderLine>
                                        .CreateListOfSize(5)
                                        .All()
                                        .Do(ol =>
                                            {
                                                ol.Cart = null;
                                                ol.CartId = null;
                                                ol.Order = o;
                                                ol.OrderId = o.Id;
                                                var parentCategory = Pick<Category>.RandomItemFrom(categories);
                                                var childCategory = Pick<Category>.RandomItemFrom(parentCategory.Children.ToList());
                                                var acceptableProducts = childCategory.Products.Where(product => product.Stock > 0).ToList();
                                                var randomProduct = Pick<Product>.RandomItemFrom(acceptableProducts);
                                                ol.Product = randomProduct;
                                                ol.ProductId = randomProduct.Id;
                                                ol.Quantity = SelectQuantity(ol, generator);
                                                ol.Status = Status.Shipping;
                                                ol.Tracking = ol.Status == Status.Shipping ? generator.Guid().ToString() : null;
                                            })
                                        .Build();

                                    o.Total = o.OrderLines.Sum(ol => (ol.Quantity*ol.Product.Price));
                                    o.Status = o.OrderLines.Any(ol => ol.Status != Status.Shipping) ? Status.Processing : Status.Delivered;
                                })
                            .Build();
                    })
                .Build();

            customers[0].Email = "customer";

            return customers;
        }

        private static IEnumerable<Category> CreateChildren(Category pc, IList<string> names, RandomGenerator generator)
        {
            var uniqueRandomGenerator = new UniqueRandomGenerator();

            var children = Builder<Category>
                .CreateListOfSize(5)
                .All()
                .Do(cc =>
                    {
                        var picker = new RandomItemPicker<string>(names, uniqueRandomGenerator);

                        cc.Parent = pc;
                        cc.ParentId = pc.Id;
                        cc.Name = picker.Pick().Trim();
                        cc.Description = Lorem.Sentence(generator.Next(1, 2));
                        cc.Products = CreateProducts(generator, cc);
                    })
                .Build();

            return children;
        }

        private static IList<Product> CreateProducts(RandomGenerator generator, Category cc)
        {
            var products = Builder<Product>
                .CreateListOfSize(generator.Next(1, 30))
                .All()
                .Do(p =>
                    {
                        p.Categories = new List<Category>() { cc };
                        p.Name = Lorem.Words(generator.Next(1, 3)).Aggregate((a, b) => string.Concat(a, " ", b));
                        p.Stock = generator.Next(0, 1000);
                        p.Description = Lorem.Sentence(generator.Next(1, 5));
                        p.Price = generator.Next(001m, 100m);
                        p.Created = DateTime.Now;
                        p.Modified = DateTime.Now;
                        p.ProductImages = Builder<ProductImage>
                            .CreateListOfSize(3)
                            .All()
                            .Do(pi =>
                                {
                                    pi.Product = p;
                                    pi.ProductId = p.Id;
                                    pi.Primary = false;
                                    pi.Product = p;
                                    pi.Path = "/Image" + generator.Next(1, 1000).ToString() + ".jpg";
                                })
                            .TheLast(1)
                            .Do(image =>
                                {
                                    image.Primary = true;
                                })
                            .Build();
                    })
                .Build();

            return products;
        }

        private static int SelectQuantity(OrderLine ol, RandomGenerator generator)
        {
            return (ol.Product.Stock >= 1 && ol.Product.Stock >= 20) ? ol.Quantity = generator.Next(1, 5) : 1;
        }

        private static void AddEmployeeToWebSecurity(Employee e)
        {
            WebSecurity.CreateAccount(e.Email, "password");
            Roles.AddUserToRole(e.Email, "Admin");
        }

        private static void AddCustomerToWebSecurity(Customer c)
        {
            WebSecurity.CreateAccount(c.Email, "password");
            Roles.AddUserToRole(c.Email, "Customer");
        }
    }
}