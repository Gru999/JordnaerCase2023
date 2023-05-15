using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Members
{
    public class UpdateMemberModel : PageModel
    {
        private IMemberService mService;
        [BindProperty]
        public Member MemberToUpdate { get; set; }
        public UpdateMemberModel(IMemberService memberService)
        {
            mService = memberService;
        }

        public async Task OnGetAsync(int memberId)
        {
            MemberToUpdate = await mService.GetMemberByID(memberId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await mService.UpdateMemberAsync(MemberToUpdate);
            return RedirectToPage("AllMembers");
        }
    }
}
