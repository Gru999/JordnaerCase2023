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
            //Email = HttpContext.Session.GetString("Email");
            //if(Email == null)
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
            //{
            //    DateTime closestDate = Events.OrderBy(x => x.EventDateFrom).First().EventDateFrom;
            //    foreach (var ev in Events)
            //    {
            //        DateTime tempDate = ev.EventDateFrom;
            //        if (tempDate > DateTime.Now && tempDate < closestDate)
            //        {
            //            closestDate = tempDate;
            //        }
            //    }
            //}
            //else if (DateSort == "NeEvent")
            //{
            //    Events.OrderBy(x => x.EventDateFrom).First();
            //}
            //else if (DateSort == "OlEvent")
            //{
            //    Events.OrderBy(x => x.EventDateFrom).Last();
            //}
            //else
            //{
            //    Events = await _eventService.GetAllEventsAsync();
            //}



        }
    }
}
