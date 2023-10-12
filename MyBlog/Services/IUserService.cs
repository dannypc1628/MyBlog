using MyBlog.Models;

namespace MyBlog.Services
{
    public interface IUserService
    {
        public Task<User?> GetAsync(int id);

        public int GetCount();

        public Task<ResultViewModel> LoginAsync(LoginViewModel viewModel);

        public Task<ResultViewModel> InitCreateAsync(UserViewModel viewModel);

        public Task CreateAsync(UserViewModel viewModel);

        public Task UpdateAsync(UserViewModel viewModel);
    }
}