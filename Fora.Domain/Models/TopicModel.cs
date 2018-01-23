using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Domain.Models
{
    public class TopicModel: Model
    {
        public int TopicId { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public UserModel Author { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<PostModel> Replies { get; set; } = new HashSet<PostModel>();
    }
}
