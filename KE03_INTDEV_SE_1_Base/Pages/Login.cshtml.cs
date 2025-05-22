using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                // Bewaar de gebruikersnaam in de sessie
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", Username);

                return RedirectToPage("/Index");
            }

            // Als een veld leeg is, toon een foutmelding
            ModelState.AddModelError(string.Empty, "Gebruikersnaam en wachtwoord zijn vereist.");
            return Page();
        }
    }
}
