using Instagram.Models;
using System.Linq;

namespace Instagram.Services
{
    public class FriendRequestedCondition : FriendService
    {
        private InstagramEntities _context = null;
        public FriendRequestedCondition()
        {
            //Create an instance from the Database
            _context = new InstagramEntities();
        }

        // (DONE) Check if that  user sent friend request or not to viewed profile
        public override bool Check(int userId, int friendId)
        {
            var friend = _context.FriendRequests.SingleOrDefault(c => c.UserId.Equals(userId) && c.FriendId.Equals(friendId));
            return friend != null;
        }
    }
}