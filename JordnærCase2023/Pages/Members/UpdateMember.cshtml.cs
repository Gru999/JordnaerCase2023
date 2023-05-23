using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Members
{
    public class UpdateMemberModel : PageModel
    {
        public string Message { get; set; }
        private IMemberService mService;
        private IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Member MemberToUpdate { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public string PasswordMatch { get; set; }

        public UpdateMemberModel(IMemberService memberService, IWebHostEnvironment webHostEnvironment)
        {
            mService = memberService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task OnGetAsync(int memberId)
        {
            MemberToUpdate = await mService.GetMemberByID(memberId);
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (PasswordMatch != MemberToUpdate.Password)
            {

                Message = "Passwords er ikke ens.";
                return Page();

            }
            else
            {
                if (Photo != null)
                {
                    if (MemberToUpdate.Image != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/Images/Members", MemberToUpdate.Image);
                        System.IO.File.Delete(filePath);
                    }

                    MemberToUpdate.Image = ProcessUploadedFile();
                }

                await mService.UpdateMemberAsync(MemberToUpdate);
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
