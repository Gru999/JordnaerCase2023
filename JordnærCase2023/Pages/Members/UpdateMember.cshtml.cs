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
        private IShiftTypeService stService;
        private IShiftTypeMemberService stmService;
        private IWebHostEnvironment webHostEnvironment;
        private IUserLoginService logService;

        [BindProperty]
        public Member MemberToUpdate { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public Member OldUser { get; set; }

        public Member LoggedMember { get; set; }

        [BindProperty]
        public List<ShiftType> ShiftTypes { get; set; }

        [BindProperty]
        public List<int> MemberShiftTypes { get; set; }

        [BindProperty]
        public string PasswordMatch { get; set; }

        [BindProperty]
        public string LastUrl { get; set; }

        public UpdateMemberModel(IMemberService memberService, IWebHostEnvironment webHostEnvironment, IShiftTypeService stService, IShiftTypeMemberService stmService, IUserLoginService logService)
        {
            mService = memberService;
            this.webHostEnvironment = webHostEnvironment;
            this.stService = stService;
            this.stmService = stmService;
            this.logService = logService;
        }

        public async Task OnGetAsync(int memberId, string url)
        {
            string email = HttpContext.Session.GetString("Email");
            if (!string.IsNullOrEmpty(email))
            {
                LoggedMember = logService.GetLoggedMember(email);
            }

            MemberToUpdate = await mService.GetMemberByID(memberId);
            ShiftTypes = stService.GetAllShiftTypes();
            MemberShiftTypes = await stmService.MemberShiftTypes(memberId);
            LastUrl = url;

            foreach(var item in ShiftTypes)
            {
                item.Valid = false;
            }

            if(MemberShiftTypes.Count > 0)
            {
                for (int i = 0; i < ShiftTypes.Count; i++)
                {
                    foreach (int item in MemberShiftTypes)
                    {
                        if (item == ShiftTypes[i].Id)
                        {
                            ShiftTypes[i].Valid = true;
                            break;
                        }
                    }
                }
            }
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            OldUser = await mService.GetMemberByID(MemberToUpdate.Id);
            MemberShiftTypes = await stmService.MemberShiftTypes(MemberToUpdate.Id);

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
                return result;
            }


            if (DisplayError() == true)
            {
                return Page();
            }
            else
            {
                if (MemberToUpdate.Password == null && PasswordMatch == null)
                {
                    MemberToUpdate.Password = OldUser.Password;
                }
                if (MemberToUpdate.Image == null)
                {
                    MemberToUpdate.Image = OldUser.Image;
                }
                if (Photo != null)
                {
                    if (MemberToUpdate.Image != null && MemberToUpdate.Image != "basic_pfp.png" && OldUser.Image != null && MemberToUpdate.Image != OldUser.Image)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/Images/Members", MemberToUpdate.Image);
                        System.IO.File.Delete(filePath);
                    }

                    MemberToUpdate.Image = ProcessUploadedFile();
                }

                foreach(var shiftType in ShiftTypes)
                {

                    if(shiftType.Valid == true)
                    {
                        int i = 0;

                        foreach (var existingShiftType in MemberShiftTypes)
                        {
                            if(existingShiftType == shiftType.Id)
                            {
                                i++;
                            }
                        }

                        if(i == 0)
                        {
                            await stmService.CreateShiftTypeMember(MemberToUpdate.Id, shiftType.Id);
                        }
                    }

                    if(shiftType.Valid == false)
                    {
                        foreach(var existingShiftType in MemberShiftTypes)
                        {
                            if(existingShiftType == shiftType.Id)
                            {
                                await stmService.DeleteShiftTypeMember(MemberToUpdate.Id, shiftType.Id);
                                break;
                            }
                        }
                    }
                }

                await mService.UpdateMemberAsync(MemberToUpdate);
                return Redirect(LastUrl);

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
