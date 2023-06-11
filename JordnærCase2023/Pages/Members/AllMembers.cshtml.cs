using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Members
{
    public class AllMembersModel : PageModel
    {
        public List<Member> Members { get; set; }
        private IMemberService mService;
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public IUserLoginService logService;
        private IHttpContextAccessor httpContext;
        public Member? LoggedMember { get; set; }
        public string? LoggedEmail { get; set; }
        public AllMembersModel(IMemberService memberService, IUserLoginService logService, IHttpContextAccessor httpContext)
        {
            mService = memberService;
            this.logService = logService;
            this.httpContext = httpContext;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            LoggedEmail = HttpContext.Session.GetString("Email");

            if (String.IsNullOrEmpty(LoggedEmail))
            {
                return RedirectToPage("/Login");
            }

            if (LoggedEmail != null)
            {
                LoggedMember = logService.GetLoggedMember(LoggedEmail);
            }
            else
            {
                LoggedMember = null;
            }
            

            if(FilterCriteria != null)
            {
                Members = await mService.GetMembersByName(FilterCriteria);
            }
            else
            {
                Members = await mService.GetAllMembersAsync();
            }

            if(Members.Count > 0 || Members != null)
            {
                Members = Members.OrderBy(x => x.Name).ToList();
            }

            return Page();
            
        }
    }
}
