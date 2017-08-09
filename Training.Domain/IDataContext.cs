using System.Data.Entity;

namespace Training.Domain
{
    public interface IDataContext
    {
        IDbSet<Post> Posts { get; set; }

        IDbSet<Category> Categories { get; set; }

        int SaveChanges();
    }
}
