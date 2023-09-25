using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }

        public UnitOfWork(MyBlogContext dbContext)
        {
            Context = dbContext;
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
