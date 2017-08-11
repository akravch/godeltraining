using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Training.ViewModels
{
    public class ViewModelWithCollection
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "View Model Name")]
        [Required]
        public string Name { get; set; }

        public IList<CollectionItem> Collection { get; set; }
    }
}