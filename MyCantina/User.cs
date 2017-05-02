using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCantina
{
    class User
    {
        public User()
        {
            
        }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePicturePath { get; set; }

        public List<Order> Orders { get; set; }
    }
}
