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
            int emMemberId = userLoginService.GetLoggedMember(Email).Id;
            await _eventService.DeleteEMAsync(Event.EventId, emMemberId);
            await _eventService.DeleteEventAsync(Event.EventId);
            return RedirectToPage("GetAllEvents");
        }
    }
}
