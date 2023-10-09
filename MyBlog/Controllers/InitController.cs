using Microsoft.AspNetCore.Mvc;
using MyBlog.Services;

namespace MyBlog.Controllers
{
    public class InitController : Controller
    {
        private readonly IImportService _importService;

        public InitController(IImportService importService)
        {
            _importService = importService;
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