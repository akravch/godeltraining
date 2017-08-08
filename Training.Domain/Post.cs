using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training.Domain
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public string Body { get; set; }
    }
}
