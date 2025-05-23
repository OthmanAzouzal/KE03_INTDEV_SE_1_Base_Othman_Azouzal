using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public LoginModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; } // Niet gebruikt voor authenticatie in dit voorbeeld

        public IActionResult OnGet() => Page();

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                var customer = _customerRepository.GetCustomerByEmail(Username);

                if (customer == null)
                {
                    customer = new Customer
                    {
                        Name = Username.Split('@')[0], // Eenvoudige naam afleiding
                        Email = Username,
                        Address = "Onbekend adres"
                    };
                    _customerRepository.AddCustomer(customer);
                }

                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", Username);

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Gebruikersnaam en wachtwoord zijn vereist.");
            return Page();
        }
    }
}
