using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Training.Dto
{
    public class CategoryDto : DtoEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<PostDto> Posts { get; set; } 
    }
}