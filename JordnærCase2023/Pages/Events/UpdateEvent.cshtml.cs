using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;

namespace JordnærCase2023.Pages.Events
{
    public class UpdateEventModel : PageModel
    {
        private IEventService _eventService;
        [BindProperty]
        public Event Event { get; set; }

        public UpdateEventModel(IEventService eventService) {
            _eventService = eventService;
        }
        public async Task OnGetAsync(int id) {
            Event = await _eventService.GetEventFromIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync() {
            //Id could have been bound like in DeleteEvent
            await _eventService.UpdateEventAsync(Event);
            return RedirectToPage("GetAllEvents");
        }
    }
}
