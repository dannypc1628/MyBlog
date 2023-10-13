using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;

namespace MyBlog.Controllers
{
    public class InitController : Controller
    {
        private readonly IImportService _importService;
        private readonly IUserService _userService;

        public InitController(IImportService importService, IUserService userService)
        {
            _importService = importService;
            _userService = userService;
        }

        public IActionResult CreateUser()
        {
            if (_userService.GetCount() > 0)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAsync(viewModel);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Login", "Login");
                }

                ModelState.AddModelError(string.Empty, result.Messages.First());
            }

            return View(viewModel);
        }

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            await _importService.StartAsync(file);
            return RedirectToAction("Index", "Home");
        }
    }
}