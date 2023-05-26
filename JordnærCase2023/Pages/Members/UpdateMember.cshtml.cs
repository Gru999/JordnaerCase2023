using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Members
{
    public class UpdateMemberModel : PageModel
    {
        public string MatchingPasswordMessage { get; set; }
        public string UniqueEmailMessage { get; set; }
        private IMemberService mService;
        private IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Member MemberToUpdate { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public Member OldUser { get; set; }

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
            OldUser = await mService.GetMemberByID(MemberToUpdate.Id);

            List<Member> AllMembers = await mService.GetAllMembersAsync();

            bool UniqueEmail()
            {
                if (AllMembers.Count == 0)
                {
                    return true;
                }
                else
                {

                    foreach (var member in AllMembers)
                    {
                        if (member.Email == MemberToUpdate.Email && MemberToUpdate.Email != OldUser.Email)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            bool DisplayError()
            {
                bool result = false;
                if (PasswordMatch != MemberToUpdate.Password)
                {
                    MatchingPasswordMessage = "Passwords er ikke ens.";
                    result = true;

                }
                if (UniqueEmail() == false)
                {
                    UniqueEmailMessage = "Denne email er allerede registretet.";
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }


            if (DisplayError() == true)
            {
                return Page();
            }
            else
            {
                if (MemberToUpdate.Password == null)
                {
                    MemberToUpdate.Password = OldUser.Password;
                }
                if (MemberToUpdate.Image == null)
                {
                    MemberToUpdate.Image = OldUser.Image;
                }
                if (Photo != null)
                {
                    if (MemberToUpdate.Image != null && MemberToUpdate.Image != "basic_pfp.png" && OldUser.Image != null)
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
