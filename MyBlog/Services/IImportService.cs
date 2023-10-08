namespace MyBlog.Services
{
    public interface IImportService
    {
        public Task StartAsync(IFormFile file);
    }
}