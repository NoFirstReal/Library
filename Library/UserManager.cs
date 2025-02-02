using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class UserManager
    {
        private List<User> users;

        public UserManager()
        {
            users = new List<User>
            {
                new User("admin", "1", true),
                new User("user", "2", false)
            };
        }

        public User Authenticate(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user != null && user.CheckPassword(password))
            {
                return user;
            }
            return null;
        }
    }
}
