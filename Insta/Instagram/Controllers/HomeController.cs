using Instagram.Models;
using Instagram.Services;
using Instagram.ViewModels;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
namespace Instagram.Controllers
{
    public class HomeController : Controller
    {
        Service _service = null;

        public HomeController(Service service)
        {
            _service = service;
        }
        [Authorize]
        public ActionResult Profile(string user)
        {
            //Prevent users from accessing other's profiles
            if (Session["UserName"].Equals(user))
            {
                User userProfile = new User();
                List<Friend> userFriends = new List<Friend>();

                userProfile = _service.userService.GetUserByEmail(user);
                userFriends = _service.friendCondition.GetFriends(userProfile.UserId);


                UserFriendViewModel userFriend = new UserFriendViewModel()
                {
                    user = userProfile,
                    friends = userFriends
                };
                return View(userFriend);
            }
            else
            {
                return RedirectToAction("Login", "Security");
            }
        }


        [Authorize]
        public ActionResult EditProfile(string user)
        {
            //Prevent users from accessing other's profiles
            if (Session["UserName"].Equals(user))
            {
                User userProfile = new User();
                userProfile = _service.userService.GetUserByEmail(user);
                return View(userProfile);
            }
            else
            {
                return RedirectToAction("Login", "Security");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(User user, HttpPostedFileBase file)
        {
            User userLogged = _service.userService.GetUserByEmail(Session["UserName"].ToString());

            if (file != null)
            {
                //Save file on the server
                file.SaveAs(HttpContext.Server.MapPath("~/Content/images/profileImage/") + file.FileName);
                //set photo name with file name
                user.ProfileImage = file.FileName;
            }


            user.Username = userLogged.Username;
            user.UserId = userLogged.UserId;

            _service.userService.EditUser(user);

            //Prevent users from accessing other's profiles
            return RedirectToAction("Profile", "Home", new { user = user.Email });
        }

    }
}