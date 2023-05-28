using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Services;

namespace JordnærCase2023.Pages.Events
{
    public class DeleteEventModel : PageModel
    {
        private IEventService _eventService;
        private IUserLoginService userLoginService;
        [BindProperty]
        public Event Event { get; set; }
        public List<Tuple<int, int, int>> EM { get; set; }
        public string Email { get; set; }
        public DeleteEventModel(IEventService eventService, IUserLoginService userLoginService)
        {
            _eventService = eventService;
            this.userLoginService = userLoginService;
        }

        public async Task<IActionResult> OnGetAsync(int id) {
            Email = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(Email))
            {
                return RedirectToPage("/Login");
            }
            Event = await _eventService.GetEventFromIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            Email = HttpContext.Session.GetString("Email");
            Member emMember = userLoginService.GetLoggedMember(Email);
            EM = await _eventService.GetAllEventMemberAsync();
            
            //Code here
            if (emMember.Admin) {
                await _eventService.DeleteEMOwnerAdminAsync(Event.EventId);
                await _eventService.DeleteEventAsync(Event.EventId);
            }


            await _eventService.DeleteEMAsync(emMember.Id, Event.EventId);
            await _eventService.DeleteEventAsync(Event.EventId);
            return RedirectToPage("GetAllEvents");
        }
    }
}
