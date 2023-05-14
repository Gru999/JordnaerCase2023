using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;

namespace JordnærCase2023.Pages.Events
{
    public class CreateEventModel : PageModel
    {
        private IEventService _eventService;
        [BindProperty]
        public Event Event { get; set; }
        public CreateEventModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() {
            await _eventService.CreateEventAsync(Event);
            return RedirectToPage("GetAllEvents");
        }
    }
}
 