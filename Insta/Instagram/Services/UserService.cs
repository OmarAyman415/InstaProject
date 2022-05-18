using Instagram.Models;
using Instagram.ViewModels;
using System.Linq;

namespace Instagram.Services
{
    public class UserService : IUserService
    {

        private InstagramEntities _context = null;

        public UserService()
        {
            _context = new InstagramEntities();
        }


        public void SaveUserToDB(RegisterViewModel model)
        {
            User userDetails = new User();
            userDetails.Username = model.UserName;
            userDetails.FirstName = model.FirstName;
            userDetails.LastName = model.LastName;
            userDetails.Email = model.Email;
            userDetails.UserPassword = model.UserPassword;
            userDetails.Mobile = model.Mobile;
            userDetails.ProfileImage = model.ProfileImage;
            //Add registed Data to User table
            _context.Users.Add(userDetails);

            //Save changes to Database
            _context.SaveChanges();
        }

        public void EditUser(User userData)
        {

            User user = _context.Users.SingleOrDefault(c => c.UserId.Equals(userData.UserId));
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            user.Mobile = userData.Mobile;
            user.Email = userData.Email;
            if (userData.ProfileImage != null)
            {
                user.ProfileImage = userData.ProfileImage;
            }
            _context.SaveChanges();
        }

        // (DONE) 
        public bool IsValidUser(LoginViewModel model)
        {
            User user = null;
            user = _context.Users.SingleOrDefault(c => c.Email.Equals(model.Username) && c.UserPassword.Equals(model.Password));

            return user != null;

        }

        // (DONE) Get User Data by his Email 
        public User GetUserByEmail(string userEmail)
        {

            User user = null;
            user = _context.Users.SingleOrDefault(c => c.Email.Equals(userEmail));
            return user;
        }

        // (DONE) Get User Data by his Id 
        public User GetUserById(int userId)
        {

            User user = null;
            user = _context.Users.SingleOrDefault(c => c.UserId.Equals(userId));
            return user;
        }

        // (DONE) Get User Data by his Id 
        public User GetUserByUsername(string username)
        {

            User user = null;
            user = _context.Users.SingleOrDefault(c => c.Username.Equals(username));
            return user;
        }

        // (DONE) Get User's email 
        public string GetUserEmail(int id)
        {

            User user = null;
            user = _context.Users.SingleOrDefault(c => c.UserId.Equals(id));
            return user.Email;
        }

        // (DONE) Get User's ID 
        public int GetUserId(string email)
        {

            User user = null;
            user = _context.Users.SingleOrDefault(c => c.Email.Equals(email));
            return user.UserId;
        }
    }
}