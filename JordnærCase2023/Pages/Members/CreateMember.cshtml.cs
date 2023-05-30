using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Models;
using JordnærCase2023.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace JordnærCase2023.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        public string UniqueEmailMessage { get; set; }
        public string MatchingPasswordMessage { get; set; }

        [BindProperty]
        public Member newMember { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public string PasswordMatch { get; set; }

        [BindProperty]
        public List<ShiftType> MemberShiftTypes { get; set; }

        public IMemberService mService;
        private IShiftTypeService stService;
        private IShiftTypeMemberService stmService;
        private IWebHostEnvironment webHostEnvironment;


        public CreateMemberModel(IMemberService mService, IShiftTypeService stService, IShiftTypeMemberService stmService, IWebHostEnvironment webHostEnvironment)
        {
            this.mService = mService;
            this.stService = stService;
            this.stmService = stmService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async void OnGet()
        {
            MemberShiftTypes = stService.GetAllShiftTypes();

            foreach (var item in MemberShiftTypes)
            {
                item.Valid = false;
            }
        }

        


        public async Task<IActionResult> OnPostAsync()
        {
            List<Member> AllMembers = await mService.GetAllMembersAsync();

            bool UniqueEmail()
            {
                if(AllMembers.Count == 0)
                {
                    return true;
                }
                else
                {

                    foreach (var member in AllMembers)
                    {
                        if (member.Email == newMember.Email)
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
                if (PasswordMatch != newMember.Password)
                {
                    MatchingPasswordMessage = "Passwords er ikke ens.";
                    result = true;

                }
                if (UniqueEmail() == false)
                {
                    UniqueEmailMessage = "Denne email er allerede registretet.";
                    result = true;
                }
                return result;
            }



            if (DisplayError() == true)
            {
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
                
                newMember = await mService.GetNewestMember();
                foreach (var item in MemberShiftTypes)
                {
                    if (item.Valid == true)
                    {
                        await stmService.CreateShiftTypeMember(newMember.Id, item.Id);
                    }
                }

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
