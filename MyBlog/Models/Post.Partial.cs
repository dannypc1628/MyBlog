namespace MyBlog.Models
{
    public partial class Post
    {
        public static Post NewPost()
        {
            var newPost = new Post();
            newPost.Id = 0;
            newPost.Title = string.Empty;
            newPost.Content = string.Empty;
            newPost.FilteredContent = string.Empty;
            newPost.UpdateDate = new DateTime();
            newPost.PublishDate = new DateTime();
            newPost.Path = string.Empty;
            newPost.Status = string.Empty;
            newPost.OgDescription = string.Empty;
            newPost.OgTitle = string.Empty;
            newPost.OgKeywords = string.Empty;
            newPost.OgImage = string.Empty;

            return newPost;
        }
    }
}