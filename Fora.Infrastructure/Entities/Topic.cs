using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fora.Infrastructure.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<Post> Replies { get; set; } = new HashSet<Post>();
    }
}
