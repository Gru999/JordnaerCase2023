using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;

namespace JordnærCase2023.Services
{
    public class UserLoginService : IUserLoginService
    {
        public List<Member> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Member VerifyUser(string username, string password)
        {
            foreach (var member in GetAllUsers())
            {
                if (username.Equals(member.Name) && password.Equals(member.Password))
                {
                    return member;
                }
            }
            return null;
        }

    }
}
