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
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public AllMembersModel(IMemberService memberService)
        {
            mService = memberService;
        }
        public async Task OnGetAsync()
        {
            if(FilterCriteria != null)
            {
                Members = await mService.GetMembersByName(FilterCriteria);
            }
            else
            {
                Members = await mService.GetAllMembersAsync();
            }
        }
    }
}
