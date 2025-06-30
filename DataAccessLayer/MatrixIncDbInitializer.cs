using System;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Models;

public static class MatrixIncDbInitializer
{
    public static void Initialize(MatrixIncDbContext context)
    {
        // Kijk of er al klanten zijn
        if (context.Customers.Any())
        {
            return;   // DB is al gevuld
        }

            // TODO: Hier moet ik nog wat namen verzinnen die betrekking hebben op de matrix.
            // - Denk aan de m3 boutjes, moertjes en ringetjes.
            // - Denk aan namen van schepen
            // - Denk aan namen van vliegtuigen            
            var customers = new Customer[]
            {
            new Customer { Name = "Neo", Address = "123 Elm St" },
            new Customer { Name = "Morpheus", Address = "456 Oak St" },
            new Customer { Name = "Trinity", Address = "789 Pine St" },
            new Customer { Name = "Agent Smith", Address = "102 Matrix St" },
            new Customer { Name = "Oracle", Address = "55 Oracle St" },
            new Customer { Name = "Cypher", Address = "900 Betrayal Ave" }
            };
            context.Customers.AddRange(customers);

        var orders = new Order[]
        {
            new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01") },
            new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01") },
            new Order { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01") },
            new Order { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01") }
        };
        context.Orders.AddRange(orders);

        context.SaveChanges(); // Dit is belangrijk voor het opslaan van klanten en orders

        // Haal de producten op uit de database zodat we de juiste Id's kunnen gebruiken voor de reviews
        var products = context.Products.ToList();

        var reviews = new Review[]
{
    new Review { ProductId = products[0].Id, UserName = "Neo", Comment = "Geweldig product!", Rating = 5, CreatedAt = DateTime.Now },
    new Review { ProductId = products[1].Id, UserName = "Morpheus", Comment = "Niet slecht, maar kan beter.", Rating = 3, CreatedAt = DateTime.Now },
    new Review { ProductId = products[2].Id, UserName = "Trinity", Comment = "Ik ben er niet zo tevreden mee.", Rating = 2, CreatedAt = DateTime.Now },
    new Review { ProductId = products[3].Id, UserName = "Agent Smith", Comment = "Te duur voor wat het is.", Rating = 2, CreatedAt = DateTime.Now },
    new Review { ProductId = products[4].Id, UserName = "Neo", Comment = "Handig gereedschap, maar mist wat accessoires.", Rating = 4, CreatedAt = DateTime.Now },
    new Review { ProductId = products[5].Id, UserName = "Morpheus", Comment = "Werkt prima voor de prijs.", Rating = 4, CreatedAt = DateTime.Now },
    new Review { ProductId = products[6].Id, UserName = "Trinity", Comment = "Had beter gekund, maar doet het werk.", Rating = 3, CreatedAt = DateTime.Now },
    new Review { ProductId = products[7].Id, UserName = "Neo", Comment = "Goed product, doet wat het belooft.", Rating = 5, CreatedAt = DateTime.Now },
    new Review { ProductId = products[8].Id, UserName = "Morpheus", Comment = "Werkt perfect, echt aan te raden.", Rating = 5, CreatedAt = DateTime.Now },
    new Review { ProductId = products[9].Id, UserName = "Trinity", Comment = "Zeker handig, maar iets te prijzig.", Rating = 4, CreatedAt = DateTime.Now }
};


        context.Reviews.AddRange(reviews);
        context.SaveChanges(); // Sla de reviews op

        context.Database.EnsureCreated();
    }
}
