using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;

namespace JordnærCase2023.Pages.Events
{
    public class GetAllEventsModel : PageModel
    {
        private IEventService _eventService;
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Sort { get; set; }
        public List<Event> Events { get; set; }
        public GetAllEventsModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public async Task<IActionResult> OnGetAsync(/*string dateSort*/)
        {
            string email = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Login");
            }

            if (!String.IsNullOrEmpty(FilterCriteria)) {
                Events = await _eventService.GetEventsByNameAsync(FilterCriteria);
            } else {
                Events = await _eventService.GetAllEventsAsync();
            }



            if (Sort == "UEvent")
            {
                DateTime closestDate = Events.OrderBy(x => x.EventDateFrom).First().EventDateFrom;
                foreach (var ev in Events)
                {
                    DateTime tempDate = ev.EventDateFrom;
                    if (tempDate > DateTime.Now && tempDate < closestDate)
                    {
                        closestDate = tempDate;
                    }
                }
                Events = Events.Where(x => x.EventDateFrom == closestDate).ToList();
            }
            else if (Sort == "JoinedsEvent")
            {
                Events = Events.OrderBy(x => x.EventDateFrom).ToList();
            }
            //else if (DateSort == "OEvent")
            //{
            //    Events = Events.OrderByDescending(x => x.EventDateFrom).ToList();
            //}
            else
            {
                Events = await _eventService.GetAllEventsAsync();
            }


            return Page();
        }
    }
}
