using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace JordnærCase2023.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IUserLoginService loginService;
        private IHttpContextAccessor httpContext;
        public string Email { get; set; }
        public Member LoggedUser { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IUserLoginService loginService, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            this.loginService = loginService;
            this.httpContext = httpContext;
        }

        public void OnGet()
        {
            Email = httpContext.HttpContext.Session.GetString("Email");
            if(Email != null)
            {
                LoggedUser = loginService.GetLoggedMember(Email);
            }
        }
    }
}