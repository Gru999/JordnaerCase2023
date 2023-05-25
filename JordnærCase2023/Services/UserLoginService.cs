using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;

namespace JordnærCase2023.Services
{
    public class UserLoginService : IUserLoginService
    {
        private IMemberService _memberService;
        public UserLoginService(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public async Task<List<Member>> GetAllUsers()
        {
            return await _memberService.GetAllMembersAsync();
        }

        public Member GetLoggedMember(string email) {
            if (email != null)
            {
                return GetAllUsers().Result.FirstOrDefault(m => m.Email == email);
            }
            else {
                return null;
            }
        }

        public Member VerifyUser(string email, string password)
        {
            foreach (var member in GetAllUsers().Result)
            {
                if (email.Equals(member.Email) && password.Equals(member.Password))
                {
                    return member;
                }
            }
            return null;
        }
    }
}
