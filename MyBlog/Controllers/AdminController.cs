using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;

namespace MyBlog.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IPostService postService, IWebHostEnvironment webHostEnvironment)
        {
            _postService = postService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            _postService.CreateAsync(post);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdatePost(int id)
        {
            var post = await _postService.GetAsync(id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            await _postService.UpdateAsync(post);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                var extenrsion = Path.GetExtension(image.FileName);
                var fileName = $"{Guid.NewGuid()}{extenrsion}";

                using (var stream = System.IO.File.Create($"{imagePath}/{fileName}"))
                {
                    await image.CopyToAsync(stream);
                }

                var webPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/image/{fileName}";

                ModelState.AddModelError("", webPath);
            }

            return View();
        }
    }
}
