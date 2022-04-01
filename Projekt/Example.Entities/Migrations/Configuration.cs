namespace Example.Entities.Migrations
{
    using Example.Entities.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Example.Entities.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Example.Entities.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if(!context.Categories.Any())
            {
                var category = new Category()
                {
                    Name = "Procesor"
                };

                var category2 = new Category()
                {
                    Name = "Płyta główna"
                };

                var category3 = new Category()
                {
                    Name = "Dysk twardy"
                };

                var category4 = new Category()
                {
                    Name = "Monitor"
                };

                var category5 = new Category()
                {
                    Name = "Klawiatura"
                };

                var category6 = new Category()
                {
                    Name = "Mysz komputerowa"
                };

                context.Categories.Add(category);
                context.Categories.Add(category2);
                context.Categories.Add(category3);
                context.Categories.Add(category4);
                context.Categories.Add(category5);
                context.Categories.Add(category6);
                context.SaveChanges();

                if (!context.Products.Any())
                {
                    var productP1 = new Product()
                    {
                        Name = "Procesor numer 1",
                        CategoryId = category.Id,
                        Price = 100
                    };

                    var productP2 = new Product()
                    {
                        Name = "Procesor numer 2",
                        CategoryId = category.Id,
                        Price = 200
                    };

                    var productP3 = new Product()
                    {
                        Name = "Procesor numer 3",
                        CategoryId = category.Id,
                        Price = 350
                    };

                    var productP4 = new Product()
                    {
                        Name = "Procesor numer 4",
                        CategoryId = category.Id,
                        Price = 500
                    };


                    context.Products.Add(productP1);
                    context.Products.Add(productP2);
                    context.Products.Add(productP3);
                    context.Products.Add(productP4);


                    var productPG1 = new Product()
                    {
                        Name = "Płyta główna numer 1",
                        CategoryId = category2.Id,
                        Price = 300
                    };

                    var productPG2 = new Product()
                    {
                        Name = "Płyta główna numer 2",
                        CategoryId = category2.Id,
                        Price = 450
                    };

                    var productPG3 = new Product()
                    {
                        Name = "Płyta główna numer 3",
                        CategoryId = category2.Id,
                        Price = 550
                    };


                    context.Products.Add(productPG1);
                    context.Products.Add(productPG2);
                    context.Products.Add(productPG3);


                    var productDT1 = new Product()
                    {
                        Name = "Dysk twardy numer 1",
                        CategoryId = category3.Id,
                        Price = 200
                    };

                    var productDT2 = new Product()
                    {
                        Name = "Dysk twardy numer 2",
                        CategoryId = category3.Id,
                        Price = 400
                    };


                    context.Products.Add(productDT1);
                    context.Products.Add(productDT2);


                    var productM1 = new Product()
                    {
                        Name = "Monitor numer 1",
                        CategoryId = category4.Id,
                        Price = 500
                    };

                    var productM2 = new Product()
                    {
                        Name = "Monitor numer 2",
                        CategoryId = category4.Id,
                        Price = 600
                    };

                    var productM3 = new Product()
                    {
                        Name = "Monitor numer 3",
                        CategoryId = category4.Id,
                        Price = 650
                    };

                    var productM4 = new Product()
                    {
                        Name = "Monitor numer 4",
                        CategoryId = category4.Id,
                        Price = 700
                    };


                    context.Products.Add(productM1);
                    context.Products.Add(productM2);
                    context.Products.Add(productM3);
                    context.Products.Add(productM4);


                    var productK1 = new Product()
                    {
                        Name = "Klawiatura numer 1",
                        CategoryId = category5.Id,
                        Price = 20
                    };

                    var productK2 = new Product()
                    {
                        Name = "Klawiatura numer 2",
                        CategoryId = category5.Id,
                        Price = 35
                    };

                    var productK3 = new Product()
                    {
                        Name = "Klawiatura numer 3",
                        CategoryId = category5.Id,
                        Price = 60
                    };

                    var productK4 = new Product()
                    {
                        Name = "Klawiatura numer 4",
                        CategoryId = category5.Id,
                        Price = 100
                    };

                    var productK5 = new Product()
                    {
                        Name = "Klawiatura numer 5",
                        CategoryId = category5.Id,
                        Price = 170
                    };

                    var productK6 = new Product()
                    {
                        Name = "Klawiatura numer 6",
                        CategoryId = category5.Id,
                        Price = 250
                    };


                    context.Products.Add(productK1);
                    context.Products.Add(productK2);
                    context.Products.Add(productK3);
                    context.Products.Add(productK4);
                    context.Products.Add(productK5);
                    context.Products.Add(productK6);


                    var productMK1 = new Product()
                    {
                        Name = "Mysz komputerowa numer 1",
                        CategoryId = category6.Id,
                        Price = 10
                    };

                    var productMK2 = new Product()
                    {
                        Name = "Mysz komputerowa numer 2",
                        CategoryId = category6.Id,
                        Price = 15
                    };

                    var productMK3 = new Product()
                    {
                        Name = "Mysz komputerowa numer 3",
                        CategoryId = category6.Id,
                        Price = 30
                    };

                    var productMK4 = new Product()
                    {
                        Name = "Mysz komputerowa numer 4",
                        CategoryId = category6.Id,
                        Price = 45
                    };

                    var productMK5 = new Product()
                    {
                        Name = "Mysz komputerowa numer 5",
                        CategoryId = category6.Id,
                        Price = 80
                    };

                    var productMK6 = new Product()
                    {
                        Name = "Mysz komputerowa numer 6",
                        CategoryId = category6.Id,
                        Price = 110
                    };

                    var productMK7 = new Product()
                    {
                        Name = "Mysz komputerowa numer 7",
                        CategoryId = category6.Id,
                        Price = 150
                    };

                    var productMK8 = new Product()
                    {
                        Name = "Mysz komputerowa numer 8",
                        CategoryId = category6.Id,
                        Price = 160
                    };

                    var productMK9 = new Product()
                    {
                        Name = "Mysz komputerowa numer 9",
                        CategoryId = category6.Id,
                        Price = 190
                    };

                    var productMK10 = new Product()
                    {
                        Name = "Mysz komputerowa numer 10",
                        CategoryId = category6.Id,
                        Price = 240
                    };


                    context.Products.Add(productMK1);
                    context.Products.Add(productMK2);
                    context.Products.Add(productMK3);
                    context.Products.Add(productMK4);
                    context.Products.Add(productMK5);
                    context.Products.Add(productMK6);
                    context.Products.Add(productMK7);
                    context.Products.Add(productMK8);
                    context.Products.Add(productMK9);
                    context.Products.Add(productMK10);


                    context.SaveChanges();

                }            
            }
        }
    }
}
