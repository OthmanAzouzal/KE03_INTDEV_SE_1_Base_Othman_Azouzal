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
        [BindProperty] public string Email { get; set; } // Nieuwe property voor e-mail
        [BindProperty] public string Password { get; set; } // Niet gebruikt voor authenticatie in dit voorbeeld
        [BindProperty] public string Address { get; set; } // Nieuwe property voor adres

        public IActionResult OnGet() => Page();

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Address) && !string.IsNullOrWhiteSpace(Email))
            {
                var customer = _customerRepository.GetCustomerByEmail(Email); // Zoek klant op basis van e-mail

                if (customer == null)
                {
                    customer = new Customer
                    {
                        Name = Username.Split('@')[0], // Eenvoudige naam afleiding
                        Email = Email, // Sla de e-mail op
                        Address = Address // Sla het adres op
                    };
                    _customerRepository.AddCustomer(customer);
                }
                else
                {
                    customer.Address = Address; // Als de klant al bestaat, update dan het adres
                    customer.Email = Email; // Update de e-mail als deze gewijzigd is
                    _customerRepository.UpdateCustomer(customer); // Veronderstel dat er een UpdateCustomer methode is
                }

                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", Username);

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Gebruikersnaam, e-mail, wachtwoord en adres zijn vereist.");
            return Page();
        }
    }
}
