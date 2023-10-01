using MyBlog.Models;

namespace MyBlog.Services
{
    public interface IPostService
    {
        List<Post> GetAll(int page = 1, int pageSize = 10);

        Task<Post?> GetAsync(int id);

        Task CreateAsync(Post post);

        Task UpdateAsync(Post post);

        void Delete(int id);
    }
}
