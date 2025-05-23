using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class CartItem
    {
        // Optioneel: maak nullable, want of product of part is set
        public int ProductId { get; set; }
        public int? PartId { get; set; }
        public string ProductName { get; set; } = string.Empty; 

        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }

        // Toevoegen: een bool om te checken of het een product is
        public bool IsProduct { get; set; }
    }


}

