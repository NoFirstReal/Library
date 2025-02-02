using System;
using System.Collections.Generic;

namespace Library
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Храним хеш пароля
        public bool IsAdmin { get; set; } // true — админ, false — обычный пользователь

        public User(string username, string password, bool isAdmin)
        {
            Username = username;
            PasswordHash = HashPassword(password);
            IsAdmin = isAdmin;
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password)); // Простое хеширование (лучше использовать BCrypt)
        }

        public bool CheckPassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }
    }
}
