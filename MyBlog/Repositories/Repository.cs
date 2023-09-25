using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyBlog.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private DbSet<T>? _dbSet;
        private DbSet<T> DbSet => _dbSet ??= UnitOfWork.Context.Set<T>();

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await DbSet.SingleOrDefaultAsync(filter);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return GetAll().Where(filter);
        }
    }
}
