using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fora.Domain.Models
{
    public class PostModel: Model
    {
        public int PostId { get; set; }

        public string UserId { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public string Body { get; set; }
        public UserModel User { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
