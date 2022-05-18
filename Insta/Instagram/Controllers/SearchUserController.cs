using Instagram.Services;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class SearchUserController : Controller
    {
        Service _service = null;

        public SearchUserController(Service service)
        {
            _service = service;
        }
        // GET: Search
        [HttpPost]
        public ActionResult SearchUser(string searchName)
        {
            if (_service.userService.GetUserByEmail(searchName) != null)
            {
                return RedirectToAction("Friend", "Friend", new { user = Session["UserName"].ToString(), visitedUser = searchName });
            }
            else if (_service.userService.GetUserByUsername(searchName) != null)
            {
                return RedirectToAction("Friend", "Friend", new { user = Session["UserName"].ToString(), visitedUser = _service.userService.GetUserByUsername(searchName).Email });
            }
            else
            {
                return RedirectToAction("Profile", "Home", new { user = Session["UserName"].ToString() });
            }
        }
    }
}