using System.Collections.Generic;

namespace MyCantina
{
    public class User
    {
        public User(string name, string login, string passwordHash)
        {
            Name = name;
            Login = login;
            PasswordHash = passwordHash;
        }

        public User()
        {
            
        }

        public int UserID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public List<Order> Orders { get; set; }
    }
}
