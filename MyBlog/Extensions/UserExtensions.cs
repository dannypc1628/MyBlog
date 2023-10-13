using MyBlog.Models;

namespace MyBlog.Extensions
{
    public static class UserExtensions

    {
        public static User ToUser(this UserCreateViewModel model)
        {
            var user = new User();
            user.Id = model.Id;
            user.Name = model.Name;
            user.Email = model.Email;
            user.Password = model.Password;
            user.DisplayId = model.DisplayId;
            user.Image = model.Image ?? string.Empty;
            user.About = model.About ?? string.Empty;
            user.Role = model.Role;
            user.CreateDate = new DateTime();
            user.UpdateDate = user.CreateDate;

            return user;
        }

        public static UserUpdateViewModel ToUserUpdateViewModel(this User user)
        {
            var model = new UserUpdateViewModel();
            model.Id = user.Id;
            model.Name = user.Name;
            model.Email = user.Email;
            model.DisplayId = user.DisplayId;
            model.About = user.About;
            model.Image = user.Image;
            model.Role = user.Role;

            return model;
        }
    }
}