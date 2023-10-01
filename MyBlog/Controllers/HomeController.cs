using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;
using System.Diagnostics;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }


        public IActionResult Index(int p = 1)
        {
            var posts = _postService.GetAll(p, 10);
            return View(posts);
        }

        public async Task<IActionResult> Post(int id)
        {
            var post = await _postService.GetAsync(id);
            return View(post);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}