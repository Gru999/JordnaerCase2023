using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Members
{
    public class AllMembersModel : PageModel
    {
        public List<Member> Members { get; set; }
        private IMemberService mService;
        public AllMembersModel(IMemberService memberService)
        {
            mService = memberService;
        }
        public async Task OnGetAsync()
        {
            Members = await mService.GetAllMembersAsync();
        }
    }
}
