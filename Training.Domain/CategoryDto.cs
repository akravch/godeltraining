using System.ComponentModel.DataAnnotations;

namespace Training.Domain
{
    public class CategoryDto : DtoEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}