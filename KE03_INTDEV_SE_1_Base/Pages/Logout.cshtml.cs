using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Verwijder de sessie
            HttpContext.Session.Clear();

            // Redirect naar de inlogpagina
            return RedirectToPage("/Login");
        }
    }
}
