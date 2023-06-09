using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shifts
{
    public class JoinShiftModel : PageModel
    {
        [BindProperty]
        public Shift Shift { get; set; }
        public Member LoggedMember { get; set; }
        public IShiftService shiftService { get; set; }
        public IUserLoginService userService { get; set; }
        public IMemberService memberService { get; set; }

        public JoinShiftModel(IShiftService shiftService, IUserLoginService userService, IMemberService memberService)
        {
            this.shiftService = shiftService;
            this.userService = userService;
            this.memberService = memberService;
        }

        public async Task OnGet(int shiftId, int memberId)
        {
            Shift = await shiftService.GetShiftsByIdAsync(shiftId);
            Shift.MemberID = memberId;

        }
        public async Task<IActionResult> OnPost()
        {
            await shiftService.MemberToShift(Shift.ShiftID, (int)Shift.MemberID);
            return RedirectToPage("GetAllShifts");
        }
    }
}
