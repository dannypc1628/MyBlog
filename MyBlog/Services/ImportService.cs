using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Identity.Client;
using MyBlog.Enums;
using MyBlog.Models;
using MyBlog.Repositories;
using System.Globalization;

namespace MyBlog.Services
{
    public class ImportService : IImportService
    {
        private readonly IPostRepository _postRepository;

        public ImportService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task StartAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return;
            }

            var imports = ReadData(file);
            var posts = ConvertToPosts(imports);
            await CreatePostAsync(posts);
        }

        private List<ImportPost> ReadData(IFormFile file)
        {
            var list = new List<ImportPost>();
            switch (file.ContentType)
            {
                case "text/csv":
                    list = CsvReader(file);
                    break;
            }

            return list;
        }

        private List<ImportPost> CsvReader(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new List<ImportPost>();
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<ImportPost>().ToList();
                return records ?? new List<ImportPost>();
            }
        }

        private async Task CreatePostAsync(List<Post> posts)
        {
            foreach (var post in posts)
            {
                await _postRepository.CreateAsync(post);
            }

            _postRepository.UnitOfWork.Save();
        }

        private List<Post> ConvertToPosts(List<ImportPost> imports)
        {
            var list = new List<Post>();
            foreach (var import in imports)
            {
                var result = ConvertToPost(import);
                if (result != null)
                {
                    list.Add(result);
                }
            }

            return list;
        }

        private Post? ConvertToPost(ImportPost import)
        {
            var status = GetStatus(import);
            if (string.IsNullOrWhiteSpace(status))
            {
                return null;
            }

            var post = Post.NewPost();
            post.Id = import.ID;
            post.UserId = import.post_author;
            post.Title = import.post_title;
            post.Content = import.post_content;
            post.FilteredContent = import.post_content_filtered;
            post.PublishDate = import.post_date ?? new DateTime();
            post.UpdateDate = import.post_modified ?? new DateTime();
            post.ParentId = import.post_parent;
            post.Status = status;
            post.Path = import.post_name;

            return post;
        }

        private string GetStatus(ImportPost import)
        {
            if (import.post_type == "revision" && import.post_status == "inherit")
            {
                return PostStatus.編輯紀錄.ToString();
            }

            if (import.post_type == "post" && import.post_status == "draft")
            {
                return PostStatus.草稿.ToString();
            }

            if (import.post_type == "post" && import.post_status == "publish")
            {
                return PostStatus.已發佈.ToString();
            }

            return string.Empty;
        }
    }
}