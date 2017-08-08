using System.Data.Entity;

namespace Training.Domain
{
    public class DataContext : DbContext
    {
        public IDbSet<Post> Posts { get; set; }
    }
}
