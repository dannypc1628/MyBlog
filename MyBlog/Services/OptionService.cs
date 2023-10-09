using MyBlog.Models;
using MyBlog.Repositories;

namespace MyBlog.Services
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _optionRepository;

        public OptionService(IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public List<Option> GetAll()
        {
            return _optionRepository.GetAll().ToList() ?? new List<Option>();
        }

        public async Task<Option?> GetAsync(int id)
        {
            return await _optionRepository.GetAsync(x => x.Id == id);
        }

        public Option? GetByName(string nmae)
        {
            return _optionRepository.Query(x => x.Name == nmae).FirstOrDefault();
        }

        public async Task CreateAsync(Option option)
        {
            if (string.IsNullOrWhiteSpace(option.Name))
            {
                return;
            }

            var hasOption = GetByName(option.Name);

            if (hasOption != null)
            {
                return;
            }

            option.Id = 0;
            await _optionRepository.CreateAsync(option);
            _optionRepository.UnitOfWork.Save();
        }

        public string GetValue(string name)
        {
            var option = GetByName(name);

            return option?.Value ?? string.Empty;
        }

        public async Task UpdateAsync(Option newOption)
        {
            var option = await GetAsync(newOption.Id);

            if (option == null)
            {
                return;
            }
            if (option.Name != newOption.Name)
            {
                var hasNameOption = GetByName(newOption.Name);
                if (hasNameOption is not null)
                {
                    return;
                }

                option.Name = newOption.Name;
            }

            option.Value = newOption.Value;
            _optionRepository.UnitOfWork.Save();
        }

        public async Task RemoveAsync(int id)
        {
            var option = await GetAsync(id);

            if (option == null)
            {
                return;
            }

            _optionRepository.Delete(option);
            _optionRepository.UnitOfWork.Save();
        }
    }
}