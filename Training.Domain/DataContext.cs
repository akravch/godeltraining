using System.Data.Entity;

namespace Training.Domain
{
    public class DataContext : DbContext, IDataContext
    {
        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Category> Categories { get; set; }
    }
}
