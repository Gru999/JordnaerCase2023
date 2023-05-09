using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;

namespace JordnærCase2023.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        [BindProperty]
        public Member newMember { get; set; }
        public IMemberService mService;

        public CreateMemberModel(IMemberService mService)
        {
            this.mService = mService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await mService.CreateMemberAsync(newMember);
            return RedirectToPage("AllMembers");
        }
    }
}
