using Instagram.Services;
using Instagram.ViewModels;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace Instagram.Controllersdd
{
    public class SecurityController : Controller
    {
        Service _service = null;

        public SecurityController(Service service)
        {
            _service = service;
        }
        // GET: Security
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        //Log out from the application
        public ActionResult Logout()
        {
            //Terminate current session
            Session["UserInfo"] = null;
            Session.Abandon();
            // Return login Page
            return RedirectToAction("Login", "Security");
        }

        private void SaveSessionData(string userEmail, int userId)
        {
            Session["UserName"] = userEmail;
            Session["UserID"] = userId;
        }


        //----------Login----------
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginRegisterViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                //Check if the credentials is correct or not
                if (_service.userService.IsValidUser(model.loginModel))
                {
                    FormsAuthentication.SetAuthCookie(model.loginModel.Username, false);

                    SaveSessionData(model.loginModel.Username, _service.userService.GetUserId(model.loginModel.Username));

                    return RedirectToAction("Profile", "Home", new { controller = "Home", user = model.loginModel.Username });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }

            return View(model);
        }



        //----------Register----------
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(LoginRegisterViewModel model, HttpPostedFileBase file)
        {
            HttpPostedFileBase file2 = Request.Files["ImageData"];
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //Save file on the server
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/images/profileImage/") + file.FileName);
                }

                //set photo name with file name
                model.registerModel.ProfileImage = file.FileName;

                _service.userService.SaveUserToDB(model.registerModel);

                return RedirectToAction("Login", "Security");
            }
            return View(model);
        }
    }
}