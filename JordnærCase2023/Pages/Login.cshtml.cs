using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;

namespace JordnærCase2023.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string Message { get; set; }
        private IUserLoginService _userLoginService;
        public LoginModel(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        public void OnGet() { }

        public void OnGetLogout() {
            HttpContext.Session.Remove("Email");
        }
        public IActionResult OnPost()
        {
            Member loginUser = _userLoginService.VerifyUser(Email, Password);
            if (loginUser != null)
            {
                HttpContext.Session.SetString("Email", loginUser.Email);
                return RedirectToPage("Index");
            }
            else
            {
                Message = "Invalid Email or Password";
                Email = "";
                Password = "";
                return Page();
            }
        }
    }
}
