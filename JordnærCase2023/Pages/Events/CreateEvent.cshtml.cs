using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Hosting;

namespace JordnærCase2023.Pages.Events
{
    public class CreateEventModel : PageModel
    {
        private IEventService _eventService;
        private IWebHostEnvironment webHostEnvironment;
        private IUserLoginService userLoginService;

        [BindProperty]
        public Event Event { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public string Email { get; set; }

        public CreateEventModel(IEventService eventService, IWebHostEnvironment webHostEnvironment, IUserLoginService userLoginService)
        {
            _eventService = eventService;
            this.webHostEnvironment = webHostEnvironment;
            this.userLoginService = userLoginService;
        }
        public IActionResult OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(Email))
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {

            if (Photo != null)
            {
                if (Event.EventImg != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/Images", Event.EventImg);
                    System.IO.File.Delete(filePath);
                }

                Event.EventImg = ProcessUploadedFile();
            }
            Email = HttpContext.Session.GetString("Email");
            await _eventService.CreateEventAsync(Event);

            int emMemberId = userLoginService.GetLoggedMember(Email).Id;
            int emEventId = _eventService.GetAllEventsAsync().Result.LastOrDefault().EventId;
            await _eventService.CreateEMConnectionAsync(emMemberId, emEventId);
            return RedirectToPage("GetAllEvents");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
 