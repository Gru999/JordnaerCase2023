using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;

namespace JordnærCase2023.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        private IMemberService mService;
        private IShiftTypeMemberService stmService;
        [BindProperty]
        public Member MemberToDelete { get; set; }
        public DeleteMemberModel(IMemberService memberService, IShiftTypeMemberService stmService)
        {
            mService = memberService;
            this.stmService = stmService;
        }

        public async Task OnGetAsync(int memberId)
        {
            MemberToDelete = await mService.GetMemberByID(memberId);
        }
        
        public async Task<IActionResult> OnPostAsync(int memberId)
        {
            List<int> ShiftTypes = await stmService.MemberShiftTypes(memberId);
            foreach(int ShiftType in ShiftTypes)
            {
                await stmService.DeleteShiftTypeMember(memberId, ShiftType);
            }
            await mService.DeleteMemberAsync(memberId);
            return RedirectToPage("AllMembers");
        }
        
    }
}
