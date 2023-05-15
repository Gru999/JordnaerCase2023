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
        public string DateSort { get; set; }
        public List<Event> Events { get; set; }
        //public string UserName { get; set; }
        public GetAllEventsModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public async Task OnGetAsync()
        {
            //UserName = HttpContext.Session.GetString("UserName");
            //if(UserName == null)
            //{
            //    return RedirectToPage("/Login");
            //}


            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Events = await _eventService.GetEventsByNameAsync(FilterCriteria);
            }
            else
            {
                Events = await _eventService.GetAllEventsAsync();
            }
            //if (DateSort == "UpEvent")
            //    Events.OrderBy(x => x.EventDateFrom).First();
            //else if (DateSort == "NeEvent")
            //    throw new Exception();
            //else
            //    throw new Exception();



        }
    }
}
