using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" or "User"

        // Default constructor
        public User()
        {
        }

        // Parameterized constructor
        public User(int userId, string username, string password, string role)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Role = role; // Can be "Admin" or "User"
        }

        // Override ToString method for better representation
        public override string ToString()
        {
            return $"UserId: {UserId}, Username: {Username}, Role: {Role}";
        }
    }
}
