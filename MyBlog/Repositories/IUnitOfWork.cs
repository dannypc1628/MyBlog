using Microsoft.EntityFrameworkCore;

namespace MyBlog.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; set; }

        void Save();
    }
}
