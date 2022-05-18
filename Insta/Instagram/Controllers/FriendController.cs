using Instagram.Models;
using Instagram.Services;
using Instagram.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Instagram.Controllers
{
    public class FriendController : Controller
    {
        Service _service = null;

        public FriendController(Service service)
        {
            _service = service;
        }

        [Authorize]
        // GET: Friend (DONE)
        public ActionResult Friend(string user, string visitedUser)
        {
            if (Session["UserName"].Equals(user))
            {
                if (user.Equals(visitedUser))
                {
                    return RedirectToAction("Profile", "Home", new { user = user });
                }
                User userProfile = new User();
                User visitedUserProfile = new User();
                List<Friend> userFriends = new List<Friend>();


                userProfile = _service.userService.GetUserByEmail(user);
                visitedUserProfile = _service.userService.GetUserByEmail(visitedUser);
                userFriends = _service.friendCondition.GetFriends(userProfile.UserId);


                
                
                UserAccessFriendViewModel userFriend = new UserAccessFriendViewModel(userProfile, visitedUserProfile, userFriends, _service.friendCondition.Check(userProfile.UserId, visitedUserProfile.UserId), _service.friendRequestedCondition.Check(userProfile.UserId, visitedUserProfile.UserId));
                return View(userFriend);
            }
            else
            {
                return RedirectToAction("Login", "Security");
            }
        }

        //Show User's Friends, so user can see their profiles and their posts (DONE)
        public ActionResult FriendList(int userId)
        {
            List<User> userFriendsData = _service.friendCondition.getFriendsUsers(userId);

            UserFriendRequestViewModel userFriends = new UserFriendRequestViewModel()
            {
                user = _service.userService.GetUserById(userId),
                friends = userFriendsData
            };
            return View(userFriends);
        }


        [Authorize]
        [HttpPost]
        //Sends a friend request to Viewed Profile (DONE)
        public ActionResult Friend(int userID, int userRequestedID)
        {
            UserAccessFriendViewModel userRequested = new UserAccessFriendViewModel(

                _service.userService.GetUserById(userID),//User views the profile
                _service.userService.GetUserById(userRequestedID),//User that owns the profile
                null,//User's FirendsList
                false,//Is user and profile owner are friends or not
                true);//Is user sent a friend request to profile's owner

            _service.friendCondition.AddFriendUserRequest(userRequested);

            return RedirectToAction("Friend", "Friend", new { user = userRequested.user.Email, visitedUser = userRequested.userFriend.Email });
        }


        //Views all Friend request sent to the user (DONE)
        public ActionResult FriendRequest(int userId)
        {
            List<User> friendRequestData = _service.friendCondition.GetFriendRequests(userId);

            UserFriendRequestViewModel userFriendRequest = new UserFriendRequestViewModel()
            {
                user = _service.userService.GetUserById(userId),
                friends = friendRequestData

            };
            return View(userFriendRequest);
        }


        //Accept or Decline friend Requests from other users (DONE)
        public ActionResult StateFriendRequest(int userId, int friendId, bool acceptanceState)
        {
            if (acceptanceState)//If true then make two users friends
            {
                _service.friendCondition.AcceptFriendRequest(userId, friendId);
            }
            else//else remove friend request 
            {
                _service.friendCondition.DeclineFriendRequest(userId, friendId);
            }
            return RedirectToAction("FriendRequest", "Friend", new { userId = userId });
        }
    }
}