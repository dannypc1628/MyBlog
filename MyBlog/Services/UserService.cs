using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MyBlog.Enums;
using MyBlog.Extensions;
using MyBlog.Models;
using MyBlog.Repositories;
using System.Security.Claims;

namespace MyBlog.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _accessor;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IHttpContextAccessor accessor)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _accessor = accessor;
        }

        public async Task<ResultViewModel> LoginAsync(LoginViewModel viewModel)
        {
            var user = _userRepository.Query(x => x.Email == viewModel.Email).FirstOrDefault();

            if (user != null
                && PasswordVerificationResult.Success == VerifyHashedPassword(user, viewModel.Password))
            {
                await CreateCookieAsync(user);
                return new ResultViewModel(true, "登入成功");
            }

            return new ResultViewModel(false, "帳號或密碼錯誤");
        }

        public async Task<ResultViewModel> CreateAsync(UserCreateViewModel viewModel)
        {
            int count = GetCount();
            if (count == 0)
            {
                viewModel.Role = (int)RoleStatus.管理員;
                await DoCreateAsync(viewModel);
                return new ResultViewModel(true, "註冊成功");
            }

            return new ResultViewModel(false, "不開放註冊其他帳號");
        }

        public async Task<User?> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(x => x.Id == id);

            return user;
        }

        private async Task DoCreateAsync(UserCreateViewModel viewModel)
        {
            var user = viewModel.ToUser();
            user.Password = viewModel.Password;
            HashUserPassword(user);
            user.Role = viewModel.Role;
            user.CreateDate = DateTime.Now;
            user.UpdateDate = user.CreateDate;
            await _userRepository.CreateAsync(user);
            _userRepository.UnitOfWork.Save();
        }

        public async Task UpdateAsync(UserBaseViewModel viewModel)
        {
            var user = await _userRepository.GetAsync(x => x.Id == viewModel.Id);
            if (user is null)
            {
                return;
            }

            user.Name = viewModel.Name;
            user.Email = viewModel.Email;
            user.DisplayId = viewModel.DisplayId;
            user.Image = viewModel.Image;
            user.About = viewModel.About;
            user.UpdateDate = DateTime.Now;
            _userRepository.UnitOfWork.Save();
        }

        public int GetCount()
        {
            var count = _userRepository.GetAll().Count();
            return count;
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

        private async Task CreateCookieAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(User.Id),user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
            };

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await _accessor.HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}