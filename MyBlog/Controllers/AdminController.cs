using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;

namespace MyBlog.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPostService _postService;

        public AdminController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            _postService.CreateAsync(post);
            return RedirectToAction("Index","Home");
        }
    }
}
