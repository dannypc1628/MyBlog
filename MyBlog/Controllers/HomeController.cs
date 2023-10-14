using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

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

        public async Task<IActionResult> Index(int p = 1)
        {
            var posts = _postService.GetAll(p, 10);
            return View(posts);
        }

        [Route("{path}")]
        public async Task<IActionResult> Post(string path)
        {
            var post = await _postService.GetByPathAsync(path);

            if (post is null)
            {
                return NotFound();
            }

            return View(post);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/sitemap.xml")]
        public IActionResult SitemapXml()
        {
            var xmlDoc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes")
             );

            XNamespace sitemap = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var urlset = new XElement(sitemap + "urlset");
            // 這段不是必須
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace image = "http://www.google.com/schemas/sitemap-image/1.1";
            XNamespace schemaLocation = "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd http://www.google.com/schemas/sitemap-image/1.1 http://www.google.com/schemas/sitemap-image/1.1/sitemap-image.xsd";
            urlset.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi));
            urlset.Add(new XAttribute(XNamespace.Xmlns + "image", image));
            urlset.Add(new XAttribute(xsi + "schemaLocation", schemaLocation));
            // 這段不是必須 End

            var posts = _postService.GetAll(1, int.MaxValue);

            foreach (var post in posts)
            {
                var loc = $"{Request.Scheme}://{Request.Host}/{post.Path}";

                var element = new XElement("url",
                    new XElement("loc", loc),
                    new XElement("lastmod", post.UpdateDate.ToString("yyyy-MM-ddThh:mmzzz", CultureInfo.InvariantCulture))
                    );
                urlset.Add(element);
            }

            xmlDoc.Add(urlset);

            return Content(xmlDoc.ToString(), "application/xml");
        }
    }
}