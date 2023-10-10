using Microsoft.AspNetCore.Identity;
using MyBlog.Enums;
using MyBlog.Models;
using MyBlog.Repositories;

namespace MyBlog.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, PasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task InitCreateAsync(UserViewModel viewModel)
        {
            viewModel.Role = (int)RoleStatus.管理員;
            await CreateAsync(viewModel);
        }

        public async Task<User?> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(x => x.Id == id);

            return user;
        }

        public async Task CreateAsync(UserViewModel viewModel)
        {
            var user = viewModel.ToUser();
            HashUserPassword(user);
            user.Role = viewModel.Role;
            user.CreateDate = DateTime.Now;
            user.UpdateDate = user.CreateDate;
            await _userRepository.CreateAsync(user);
            _userRepository.UnitOfWork.Save();
        }

        public async Task UpdateAsync(UserViewModel viewModel)
        {
            var user = await _userRepository.GetAsync(x => x.Id == viewModel.Id);
            if (user is null)
            {
                return;
            }

            user.Name = viewModel.Name;
            user.Email = viewModel.Email;
            user.Password = viewModel.Password;
            user.DisplayId = viewModel.DisplayId;
            HashUserPassword(user);
            user.UpdateDate = DateTime.Now;
            _userRepository.UnitOfWork.Save();
        }

        private void HashUserPassword(User user)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
            user.Password = hashedPassword;
        }

        private PasswordVerificationResult VerifyHashedPassword(User user, string password)
        {
            return _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        }
    }
}