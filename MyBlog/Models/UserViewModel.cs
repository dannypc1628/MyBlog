using MyBlog.Repositories;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class UserViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Compare(nameof(Password))]
        public string PasswordCheck { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string DisplayId { get; set; } = string.Empty;

        public int Role { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                yield break;
            }

            var userRepository = validationContext.GetService<IUserRepository>();
            var emailUser = userRepository.Query(x => x.Email == Email).FirstOrDefault();
            if (emailUser is not null && emailUser.Id != Id)
            {
                yield return new ValidationResult("此 Email 已存在", new[] { nameof(Email) });
            }

            var displayIdUser = userRepository.Query(x => x.DisplayId == DisplayId).FirstOrDefault();
            if (displayIdUser is not null && displayIdUser.Id != Id)
            {
                yield return new ValidationResult("此 DisplayId 已存在", new[] { nameof(DisplayId) });
            }
        }

        public User ToUser()
        {
            var user = new User();
            user.Id = Id;
            user.Name = Name;
            user.Email = Email;
            user.Password = Password;
            user.DisplayId = DisplayId;
            user.Role = Role;
            user.CreateDate = new DateTime();
            user.UpdateDate = user.CreateDate;

            return user;
        }
    }
}