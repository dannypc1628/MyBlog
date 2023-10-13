using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class UserCreateViewModel : UserBaseViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string PasswordCheck { get; set; } = string.Empty;
    }
}