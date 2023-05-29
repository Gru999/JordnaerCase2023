using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shifts
{
    public class RemoveShiftModel : PageModel
    {
        [BindProperty]
        public Shift Shift { get; set; }
        public Member LoggedMember { get; set; }
        public IShiftService shiftService { get; set; }
        public IUserLoginService userService { get; set; }
        public IMemberService memberService { get; set; }
        [BindProperty]
        public string Url { get; set; }

        public RemoveShiftModel(IShiftService shiftService, IUserLoginService userService, IMemberService memberService)
        {
            this.shiftService = shiftService;
            this.userService = userService;
            this.memberService = memberService;
        }

        public async Task OnGet(int shiftId, string url)
        {
            Shift = await shiftService.GetShiftsByIdAsync(shiftId);
            Url = url;

        }
        public async Task<IActionResult> OnPost()
        {
            await shiftService.MemberToShift(Shift.ShiftID, 0);
            return Redirect(Url);
        }
    }
}
