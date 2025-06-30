using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Look for any customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
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

            context.SaveChanges(); // Sla de reviews op

            context.Database.EnsureCreated();
        }
    }
}
