using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Training.Dto;

namespace Training.ViewModels
{
    public class EditPostViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Display(Name = "Body")]
        [Required]
        [MaxLength(2000)]
        public string Body { get; set; }

        [Display(Name = "Category")]
        public Guid SelectedCategoryId { get; set; }

        public IList<CategoryDto> Categories { get; set; }
    }
}