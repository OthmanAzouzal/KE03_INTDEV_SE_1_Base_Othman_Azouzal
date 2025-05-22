using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class BestelModel : PageModel
    {
        [BindProperty]
        public string Naam { get; set; }

        [BindProperty]
        public string Adres { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public IActionResult OnPost()
        {
            // Hier kun je de bestelling verwerken of opslaan in database

            TempData["OrderSuccess"] = "Bedankt voor je bestelling!";
            return RedirectToPage("/Winkelwagen");
        }
    }
}
