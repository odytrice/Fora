using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Infrastructure.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<Topic> Topics { get; set; } = new HashSet<Topic>();
    }
}
