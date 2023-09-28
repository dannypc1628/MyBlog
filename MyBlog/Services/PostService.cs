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
            return _postRepository.Query(x => x.ParentId == 0)?.Skip(skipCount).Take(pageSize).ToList() ?? new List<Post>();
        }

        public async Task<Post?> GetAsync(int id)
        {
            return await _postRepository.GetAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Post post)
        {
            post.UserId = 1;
            post.PublishDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            await _postRepository.CreateAsync(post);
            _postRepository.UnitOfWork.Save();
        }

        public async Task Update(Post post)
        {
            var oldPost = await GetAsync(post.Id);
            if (oldPost is null)
            {
                return;
            }

            var historyPost = oldPost.Clone();
            historyPost.ParentId = oldPost.Id;
            historyPost.Id = 0;
            historyPost.Status = PostStatus.編輯紀錄.ToString();
            await _postRepository.CreateAsync(historyPost);

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

        public async void Delete(int id)
        {
            var post = await GetAsync(id);
            if (post is null)
            {
                return;
            }

            post.Status = PostStatus.已刪除.ToString();
            _postRepository.UnitOfWork.Save();
        }
    }
}
