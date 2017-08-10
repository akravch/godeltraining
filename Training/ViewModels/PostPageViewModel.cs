using System.Collections.Generic;
using Training.Dto;

namespace Training.ViewModels
{
    public class PostPageViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string CategoryName { get; set; }

        public IList<PostDto> Posts { get; set; }
    }
}