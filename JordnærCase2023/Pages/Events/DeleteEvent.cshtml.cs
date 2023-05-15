using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;

namespace JordnærCase2023.Pages.Events
{
    public class DeleteEventModel : PageModel
    {
        private IEventService _eventService;
        [BindProperty]
        public Event Event { get; set; }
        public DeleteEventModel(IEventService eventService) {
            _eventService = eventService;
        }
        public async Task OnGetAsync(int id) {
            Event = await _eventService.GetEventFromIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(/*int id*/) {
            //Id can be bound through the frontPage commented out part, instead of going through the OnGet and then OnPost
            await _eventService.DeleteEventAsync(Event.EventId);
            return RedirectToPage("GetAllEvents");
        }
    }
}
