using System.Collections.Generic;

namespace Training.Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
