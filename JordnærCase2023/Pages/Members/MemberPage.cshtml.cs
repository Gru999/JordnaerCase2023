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
        public IShiftTypeService stService;
        public IShiftTypeMemberService stmService;
        public List<int> shiftTypeIds = new List<int>();
        public List<ShiftType> shiftTypes = new List<ShiftType>();

        public MemberPageModel(IMemberService memberService, IShiftService shiftService, IShiftTypeService stService, IShiftTypeMemberService stmService)
        {
            this.memberService = memberService;
            this.shiftService = shiftService;
            this.stService = stService;
            this.stmService = stmService;
        }


        public async Task OnGetAsync(int memberId)
        {
            Member = await memberService.GetMemberByID(memberId);
            MemberShifts = await shiftService.ShiftsByMember(memberId);
            shiftTypeIds = await stmService.MemberShiftTypes(memberId);
            foreach(int id in shiftTypeIds)
            {
                ShiftType shiftType = stService.GetShiftTypeById(id);
                shiftTypes.Add(shiftType);
            }

        }
    }
}
