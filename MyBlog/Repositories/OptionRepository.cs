using MyBlog.Models;

namespace MyBlog.Repositories
{
    public class OptionRepository : Repository<Option>, IOptionRepository
    {
        public OptionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
