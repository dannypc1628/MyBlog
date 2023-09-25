using System.Linq.Expressions;

namespace MyBlog.Repositories
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; set; }

        Task<T?> GetAsync(Expression<Func<T, bool>> filter);

        IQueryable<T> GetAll();

        IQueryable<T> Query(Expression<Func<T, bool>> filter);

        Task CreateAsync(T entity);

        void Delete(T entity);
    }
}
