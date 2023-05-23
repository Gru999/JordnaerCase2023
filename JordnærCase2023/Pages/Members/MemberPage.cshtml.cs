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

        public MemberPageModel(IMemberService memberService)
        {
            this.memberService = memberService;
        }


        public async Task OnGetAsync(int memberId)
        {
            Member = await memberService.GetMemberByID(memberId);
        }
    }
}
