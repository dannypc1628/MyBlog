using MyBlog.Models;

namespace MyBlog.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
