using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;

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

        public Member Member { get; set; }
        public List<Shift> MemberShifts = new List<Shift>();
        public List<int> shiftTypeIds = new List<int>();
        public List<ShiftType> shiftTypes = new List<ShiftType>();
        public List<Event> memberEvents = new List<Event>();

        public MemberPageModel(IMemberService memberService, IShiftService shiftService, IShiftTypeService stService, IShiftTypeMemberService stmService, IUserLoginService logService, IEventService evService)
        {
            this.memberService = memberService;
            this.shiftService = shiftService;
            this.stService = stService;
            this.stmService = stmService;
            this.logService = logService;
            this.evService = evService;
        }


        public async Task OnGetAsync(int memberId, string memberEmail)
        {
            if(memberEmail != null)
            {
                Member = logService.GetLoggedMember(memberEmail);
            }
            else
            {
                Member = await memberService.GetMemberByID(memberId);
            }

            MemberShifts = await shiftService.ShiftsByMember(Member.Id);
            shiftTypeIds = await stmService.MemberShiftTypes(memberId);
            foreach(int id in shiftTypeIds)
            {
                ShiftType shiftType = stService.GetShiftTypeById(id);
                shiftTypes.Add(shiftType);
            }

            memberEvents = await evService.GetEventsForMemberAsync(Member.Id);

        }
    }
}
