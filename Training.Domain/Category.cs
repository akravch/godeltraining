using System.Collections.Generic;

namespace Training.Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }

        // TODO: "virtual" doesn't make sense with DTOs since it will never be loaded lazy.
        public virtual ICollection<Post> Posts { get; set; }
    }
}
