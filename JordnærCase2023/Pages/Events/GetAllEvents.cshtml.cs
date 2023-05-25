using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Services;
using Microsoft.Extensions.Logging;

namespace JordnærCase2023.Pages.Events
{
    public class GetAllEventsModel : PageModel
    {
        private IEventService _eventService;
        private IUserLoginService _userLoginService;
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Sort { get; set; }
        public List<Event> Events { get; set; }
        public string Email { get; set; }
        public GetAllEventsModel(IEventService eventService, IUserLoginService userLoginService)
        {
            _eventService = eventService;
            _userLoginService = userLoginService;
        }
        public async Task<IActionResult> OnGetAsync(/*string dateSort*/)
        {
            //Login Check
            string email = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Login");
            }

            //Filter
            if (!String.IsNullOrEmpty(FilterCriteria)) {
                Events = await _eventService.GetEventsByNameAsync(FilterCriteria);
            } else {
                Events = await _eventService.GetAllEventsAsync();
            }


            //Sortering - Not done
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

        public async Task<IActionResult> OnGetJoinEvent(int eventId) {
            Email = HttpContext.Session.GetString("Email");
            int emMemberId = _userLoginService.GetLoggedMember(Email).Id;
            await _eventService.CreateEMConnectionAsync(emMemberId, eventId);
            Events = await _eventService.GetAllEventsAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetLeaveEvent(int eventId) {
            Email = HttpContext.Session.GetString("Email");
            int emMemberId = _userLoginService.GetLoggedMember(Email).Id;
            await _eventService.DeleteEMAsync(eventId, emMemberId);
            Events = await _eventService.GetAllEventsAsync();
            return Page();
        }
    }
}
