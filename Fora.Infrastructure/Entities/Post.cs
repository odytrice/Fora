using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Infrastructure.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
        public int TopicId { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
