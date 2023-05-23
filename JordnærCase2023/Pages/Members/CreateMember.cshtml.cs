using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace JordnærCase2023.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        public string Message { get; set; }

        [BindProperty]
        public Member newMember { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public string PasswordMatch { get; set; }

        public IMemberService mService;
        private IWebHostEnvironment webHostEnvironment;


        public CreateMemberModel(IMemberService mService, IWebHostEnvironment webHostEnvironment)
        {
            this.mService = mService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(PasswordMatch != newMember.Password)
            {

                Message = "Passwords er ikke ens.";
                return Page();

            }
            else
            {

                if (Photo != null)
                {
                    if (newMember.Image != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/Images/Members", newMember.Image);
                        System.IO.File.Delete(filePath);
                    }

                    newMember.Image = ProcessUploadedFile();
                }

                await mService.CreateMemberAsync(newMember);
                return RedirectToPage("AllMembers");

            }

            
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Members");
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
