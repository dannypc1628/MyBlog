using MyBlog.Enums;
using MyBlog.Extensions;
using MyBlog.Models;
using MyBlog.Repositories;

namespace MyBlog.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public List<Post> GetAll(int page = 1, int pageSize = 10)
        {
            var skipCount = (page - 1) * pageSize;
            return _postRepository.Query(x => x.ParentId == 0 && x.Status == PostStatus.已發佈.ToString())
                .OrderByDescending(x => x.PublishDate)?.Skip(skipCount).Take(pageSize).ToList() ?? new List<Post>();
        }

        public async Task<Post?> GetAsync(int id)
        {
            return await _postRepository.GetAsync(x => x.Id == id);
        }

        public async Task<Post?> GetByPathAsync(string path)
        {
            return await _postRepository.GetAsync(x => x.Path == path && x.Status == PostStatus.已發佈.ToString());
        }

        public int Count()
        {
            return _postRepository.Query(x => x.Status == PostStatus.已發佈.ToString()).Count();
        }

        public async Task CreateAsync(Post post)
        {
            前處理(post);

            post.Id = GetNewId();
            post.UserId = 1;
            post.PublishDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            await _postRepository.CreateAsync(post);
            _postRepository.UnitOfWork.Save();
        }

        public async Task UpdateAsync(Post post)
        {
            前處理(post);
            var oldPost = await GetAsync(post.Id);
            if (oldPost is null)
            {
                return;
            }
            var historyPost = oldPost.Clone();
            historyPost.ParentId = oldPost.Id;
            historyPost.Id = GetNewId();
            historyPost.Status = PostStatus.編輯紀錄.ToString();
            await _postRepository.CreateAsync(historyPost);
            _postRepository.UnitOfWork.Save();

            oldPost.Title = post.Title;
            oldPost.Content = post.Content;
            oldPost.FilteredContent = post.FilteredContent;
            oldPost.Status = post.Status;
            oldPost.OgTitle = post.OgTitle;
            oldPost.OgDescription = post.OgDescription;
            oldPost.OgKeywords = post.OgKeywords;
            oldPost.OgImage = post.OgImage;
            oldPost.UpdateDate = DateTime.Now;
            _postRepository.UnitOfWork.Save();
        }

        public async void DeleteAsync(int id)
        {
            var post = await GetAsync(id);
            if (post is null)
            {
                return;
            }

            post.Status = PostStatus.已刪除.ToString();
            _postRepository.UnitOfWork.Save();
        }

        public void 前處理(Post post)
        {
            post.Title = 字串前處理(post.Title);
            post.Content = 字串前處理(post.Content);
            post.FilteredContent = 字串前處理(post.FilteredContent);
            post.Path = 字串前處理(post.Path);
            post.OgTitle = 字串前處理(post.OgTitle);
            post.OgDescription = 字串前處理(post.OgDescription);
            post.OgKeywords = 字串前處理(post.OgKeywords);
            post.OgImage = 字串前處理(post.OgImage);
        }

        public string 字串前處理(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Trim();
        }

        private int GetNewId()
        {
            var post = _postRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault() ?? new Post();

            return post.Id + 1;
        }
    }
}