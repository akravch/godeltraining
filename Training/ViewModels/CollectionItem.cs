using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Training.ViewModels
{
    public class CollectionItem
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}