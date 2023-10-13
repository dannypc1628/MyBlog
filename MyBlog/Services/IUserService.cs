using MyBlog.Models;

namespace MyBlog.Services
{
    public interface IUserService
    {
        public Task<User?> GetAsync(int id);

        public int GetCount();

        public Task<ResultViewModel> LoginAsync(LoginViewModel viewModel);

        public Task<ResultViewModel> CreateAsync(UserCreateViewModel viewModel);

        public Task UpdateAsync(UserBaseViewModel viewModel);
    }
}