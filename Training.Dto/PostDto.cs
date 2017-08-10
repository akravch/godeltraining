using System;
using System.ComponentModel.DataAnnotations;

namespace Training.Dto
{
    public class PostDto : DtoEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Body { get; set; }

        public DateTime CreationDateTimeUtc { get; set; }

        [Required]
        public CategoryDto Category { get; set; }
    }
}
