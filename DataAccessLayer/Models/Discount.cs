using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }    // Voeg dit toe
        public string Name { get; set; }
        public decimal Percentage { get; set; } // Percentage van de korting, bijvoorbeeld 10 betekent 10% korting
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Product Product { get; set; }  // Dit maakt het mogelijk om het volledige Product op te halen

    }

}
