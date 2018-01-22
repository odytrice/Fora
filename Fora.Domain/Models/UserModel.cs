using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Domain.Models
{
    public class UserModel: Model
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
