using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Hosting;

namespace JordnærCase2023.Pages.Events
{
    public class UpdateEventModel : PageModel
    {
        private IEventService _eventService;
        private IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Event Event { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public string Email { get; set; }

        public UpdateEventModel(IEventService eventService, IWebHostEnvironment webHostEnvironment) {
            _eventService = eventService;
            this.webHostEnvironment = webHostEnvironment;
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
            if (Photo != null)
            {
                if (Event.EventImg != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/Images", Event.EventImg);
                    System.IO.File.Delete(filePath);
                }

                Event.EventImg = ProcessUploadedFile();
            }
            await _eventService.UpdateEventAsync(Event);
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
