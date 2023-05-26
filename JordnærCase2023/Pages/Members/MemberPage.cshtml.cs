using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;

namespace JordnærCase2023.Pages.Members
{
    public class MemberPageModel : PageModel
    {

        public Member Member { get; set; }
        public IMemberService memberService;
        public List<Shift> MemberShifts = new List<Shift>();
        public IShiftService shiftService;

        public MemberPageModel(IMemberService memberService, IShiftService shiftService)
        {
            this.memberService = memberService;
            this.shiftService = shiftService;
        }


        public async Task OnGetAsync(int memberId)
        {
            Member = await memberService.GetMemberByID(memberId);
            MemberShifts = await shiftService.ShiftsByMember(memberId);
        }
    }
}
