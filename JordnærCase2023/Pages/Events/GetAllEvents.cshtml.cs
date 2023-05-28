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
        public List<Tuple<int, int, int>> EMOwner { get; set; }
        public string Email { get; set; }
        public GetAllEventsModel(IEventService eventService, IUserLoginService userLoginService)
        {
            _eventService = eventService;
            _userLoginService = userLoginService;
        }
        public async Task<IActionResult> OnGetAsync()
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

            //Admin assigning check
            Email = HttpContext.Session.GetString("Email");
            int emMemberId = _userLoginService.GetLoggedMember(Email).Id;
            EMOwner = await _eventService.GetAllEventMemberAsync();
            Events = await _eventService.GetAllEventsAsync();
            Random random = new Random();
            List<Member> members = await _userLoginService.GetAllUsers();
            Member randAdmin = members.Where(m => m.Admin).OrderBy(x => random.Next()).FirstOrDefault();
            bool idMatch = EMOwner.Any(x => !Events.Any(e => e.EventId == x.Item3));
            int difId = Events.FirstOrDefault(e => !EMOwner.Any(x => x.Item3 == e.EventId))?.EventId ?? 0;
            if (difId != 0) {
                if (!idMatch)
                {
                    await _eventService.CreateEMConnectionAsync(randAdmin.Id, difId);
                    return RedirectToPage("/index");
                }
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
            await _eventService.DeleteEMAsync(emMemberId, eventId);
            Events = await _eventService.GetAllEventsAsync();
            return Page();
        }
    }
}
