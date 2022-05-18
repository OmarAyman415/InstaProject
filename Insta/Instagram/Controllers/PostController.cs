using Instagram.Models;
using Instagram.Services;
using Instagram.ViewModels;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class PostController : Controller
    {
        static int userIDGlobal;
        Service _service = null;

        public PostController(Service service)
        {
            _service = service;
        }

        [Authorize]
        // GET: Post
        public ActionResult Post(int id)
        {
            if (Session["UserID"].Equals(id))
            {
                ViewBag.ID = id;
                userIDGlobal = id;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Security");
            }
        }



        //add post
        [HttpPost]
        public ActionResult Post(Post postSent, HttpPostedFileBase file, string captionText)
        {

            if (file != null)// if user send an image, save it in the server 
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Content/images/") + file.FileName);
                Post post = new Post()
                {
                    UserId = userIDGlobal,
                    photo = file.FileName,
                    caption = captionText
                };


                _service.postService.AddPost(post);

                string userEmail = _service.userService.GetUserEmail(userIDGlobal);

                return RedirectToAction("Profile", "Home", new { user = userEmail });
            }
            return View(userIDGlobal);
        }

        public ActionResult ViewPost(int userId, int postId)
        {
            User userData = _service.userService.GetUserById(userId);
            Post postData = _service.postService.GetPost(postId);

            UserPost userPost = new UserPost()
            {
                user = userData,
                post = postData
            };
            return View(userPost);
        }
        [HttpGet]
        public ActionResult AddComment(int userCommentedId, int postId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(int userCommentedId, int postId, string comments)
        {
            var x = comments;

            Comment comment = new Comment()
            {
                PostId = postId,
                UserCommmentedId = userCommentedId,
                CommentText = comments,
                CommentDate = System.DateTime.Now
            };
            _service.commentService.AddCommnet(comment);
            return RedirectToAction("ViewPost", "Post", new { userId = userCommentedId, postId = postId });
        }

        public ActionResult AddLikeOrDislike(int userId, int postId, bool LikeState)
        {
            React newReact = new React()
            {
                PostId = postId,
                UserReactedId = userId,
                ReactState = LikeState,
            };
            React oldReact = _service.reactService.GetReact(userId, postId);
            if (oldReact != null)
            {
                if (!oldReact.ReactState.Equals(newReact.ReactState))
                {
                    _service.postService.DecrementPostReact(postId, oldReact.ReactState);
                    _service.postService.IncrementPostReact(postId, newReact.ReactState);

                    // Revert React State in Database
                    _service.reactService.UpdateReact(oldReact);
                }
            }
            else
            {
                _service.postService.IncrementPostReact(postId, newReact.ReactState);
                _service.reactService.AddReact(newReact);
            }

            return RedirectToAction("ViewPost", "Post", new { userId = userId, postId = postId });

        }

    }
}