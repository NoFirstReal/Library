using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class UserManager
    {
        private readonly DatabaseManager _dbManager;

        public UserManager()
        {
            _dbManager = new DatabaseManager();
        }

        public User Authenticate(string username, string password)
        {
            return _dbManager.GetUser(username, password);
        }
    }
}
