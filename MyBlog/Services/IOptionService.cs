using MyBlog.Models;

namespace MyBlog.Services
{
    public interface IOptionService
    {
        public List<Option> GetAll();

        public Task<Option?> GetAsync(int id);

        public Option? GetByName(string nmae);

        public Task CreateAsync(Option option);

        public string GetValue(string name);

        public Task UpdateAsync(Option newOption);

        public Task RemoveAsync(int id);
    }
}