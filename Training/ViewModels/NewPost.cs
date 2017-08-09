using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Training.Dto;

namespace Training.ViewModels
{
    public class NewPost
    {
        [Display(Name = "Body")]
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }

        // TODO: Is is possible to have CategoryDto instead of Guid?
        [Display(Name = "Category")]
        public Guid SelectedCategoryId { get; set; }

        public IList<CategoryDto> Categories { get; set; }
    }
}