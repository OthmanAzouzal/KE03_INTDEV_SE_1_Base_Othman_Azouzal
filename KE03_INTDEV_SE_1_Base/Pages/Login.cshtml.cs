using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }

        public IActionResult OnGet() => Page();

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", Username);
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Gebruikersnaam en wachtwoord zijn vereist.");
            return Page();
        }
    }
}
