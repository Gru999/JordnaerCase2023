﻿using JordnærCase2023.Models;

namespace JordnærCase2023.Interfaces
{
    public interface IUserLoginService
    {
        public Task<List<Member>> GetAllUsers();
        public Member GetLoggedMember(string email);

        public Member VerifyUser(string email, string password);
    }
}
