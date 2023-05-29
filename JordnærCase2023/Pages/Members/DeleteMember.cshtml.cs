using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Http;

namespace JordnærCase2023.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        private IMemberService mService;
        private IShiftTypeMemberService stmService;
        private IShiftService sService;
        private IUserLoginService userLoginService;
        private IEventService evService;
        private IHttpContextAccessor httpContext;
        
        [BindProperty]
        public Member MemberToDelete { get; set; }

        public DeleteMemberModel(IMemberService memberService, IShiftTypeMemberService stmService, IShiftService sService, IUserLoginService userLoginService, IHttpContextAccessor httpContext, IEventService evService)
        {
            mService = memberService;
            this.stmService = stmService;
            this.sService = sService;
            this.userLoginService = userLoginService;
            this.httpContext = httpContext;
            this.evService = evService;
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

            List<Shift> memberShifts = new List<Shift>();
            memberShifts = await sService.ShiftsByMember(memberId);
            if(memberShifts.Count > 0)
            {
                foreach(Shift shift in memberShifts)
                {
                    await sService.MemberToShift(shift.ShiftID, 0);
                }
            }

            List<Event> memberEvents = new List<Event>();
            memberEvents = await evService.GetEventsForMemberAsync(memberId);
            if(memberEvents.Count > 0)
            {
                foreach(Event item in memberEvents)
                {
                    await evService.DeleteEMAsync(memberId, item.EventId);
                }
            }

            await mService.DeleteMemberAsync(memberId);

            string email = httpContext.HttpContext.Session.GetString("Email");
            if (MemberToDelete.Email == email)
            {
                HttpContext.Session.Remove("Email");
                return RedirectToPage("/Index");

            }
            else
            {
                return RedirectToPage("AllMembers");
            }

            
        }
        
    }
}
