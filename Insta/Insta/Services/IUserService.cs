using Instagram.Models;
using Instagram.ViewModels;

namespace Instagram.Services
{
    public interface IUserService
    {
        void SaveUserToDB(RegisterViewModel model);

        void EditUser(User userData);

        //bool IsValidUser(LoginViewModel model);

        User GetUserByEmail(string userEmail);

        User GetUserById(int userId);

        User GetUserByUsername(string username);

        string GetUserEmail(int id);

        int GetUserId(string email);
    }
}