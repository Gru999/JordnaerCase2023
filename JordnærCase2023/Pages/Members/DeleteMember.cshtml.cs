using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;

namespace JordnærCase2023.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        private IMemberService mService;
        [BindProperty]
        public Member MemberToDelete { get; set; }
        public DeleteMemberModel(IMemberService memberService)
        {
            mService = memberService;
        }

        public async Task OnGetAsync(int memberId)
        {
            MemberToDelete = await mService.GetMemberByID(memberId);
        }
        
        public async Task<IActionResult> OnPostAsync(int memberId)
        {
            await mService.DeleteMemberAsync(memberId);
            return RedirectToPage("AllMembers");
        }
        
    }
}
