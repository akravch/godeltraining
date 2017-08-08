namespace Training.Domain
{
    public class Post : Entity
    {
        public string Body { get; set; }

        public virtual Category Category { get; set; }
    }
}
