using System;
using System.Collections.Generic;
using BCrypt.Net;

namespace Library
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }


        public User(string username, string password, bool isAdmin)
        {
            Username = username;
            PasswordHash = HashPassword(password);
            IsAdmin = isAdmin;
            Id = Guid.NewGuid();
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public bool CheckPassword(string password)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // Если хеш в старом формате, сравниваем напрямую (временное решение)
                return password == PasswordHash;
            }
        }
    }
}
