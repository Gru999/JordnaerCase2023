using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Services;

namespace JordnærCase2023.Pages.Members
{
    public class MemberPageModel : PageModel
    {

        public IMemberService memberService;
        public IShiftService shiftService;
        public IShiftTypeService stService;
        public IShiftTypeMemberService stmService;
        public IEventService evService;
        public IUserLoginService logService;
        public IHttpContextAccessor httpContext;

        public Member Member { get; set; }
        public string LoggedEmail { get; set; }
        public Member LoggedMember { get; set; }
        public List<Shift> MemberShifts = new List<Shift>();
        public List<int> shiftTypeIds = new List<int>();
        public List<ShiftType> shiftTypes = new List<ShiftType>();
        public List<Event> memberEvents = new List<Event>();

        public MemberPageModel(IMemberService memberService, IShiftService shiftService, IShiftTypeService stService, IShiftTypeMemberService stmService, IUserLoginService logService, IEventService evService, IHttpContextAccessor httpContext)
        {
            this.memberService = memberService;
            this.shiftService = shiftService;
            this.stService = stService;
            this.stmService = stmService;
            this.logService = logService;
            this.evService = evService;
            this.httpContext = httpContext;
        }


        public async Task OnGetAsync(int memberId)
        {

            LoggedEmail = HttpContext.Session.GetString("Email");
            LoggedMember = logService.GetLoggedMember(LoggedEmail);

            if(memberId != 0)
            {
                Member = await memberService.GetMemberByID(memberId);
            }
            else
            {
                Member = logService.GetLoggedMember(LoggedEmail);
            }

            MemberShifts = await shiftService.ShiftsByMember(Member.Id);
            shiftTypeIds = await stmService.MemberShiftTypes(Member.Id);
            foreach(int id in shiftTypeIds)
            {
                ShiftType shiftType = stService.GetShiftTypeById(id);
                shiftTypes.Add(shiftType);
            }

            memberEvents = await evService.GetEventsForMemberAsync(Member.Id);

        }

        public async Task<IActionResult> OnGetLeaveEvent(int eventId, int memberId, string url)
        {
            Member = await memberService.GetMemberByID(memberId);
            await evService.DeleteEMAsync(Member.Id, eventId);
            memberEvents = await evService.GetEventsForMemberAsync(Member.Id);
            return Redirect(url);
        }
    }
}
