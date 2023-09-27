using MyBlog.Models;

namespace MyBlog.Extensions
{
    public static class PostExtensions
    {
        public static Post Clone(this Post post)
        {
            var newPost = new Post();
            newPost.Id = post.Id;
            newPost.Title = post.Title;
            newPost.Content = post.Content;
            newPost.FilteredContent = post.FilteredContent;
            newPost.UpdateDate = post.UpdateDate;
            newPost.PublishDate = post.PublishDate;
            newPost.Status = post.Status;
            newPost.OgDescription = post.OgDescription;
            newPost.OgTitle = post.OgTitle;
            newPost.OgKeywords = post.OgKeywords;
            newPost.OgImage = post.OgImage;

            return newPost;
        }
    }
}
