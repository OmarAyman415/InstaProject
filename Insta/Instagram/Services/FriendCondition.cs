using Instagram.Models;
using Instagram.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Instagram.Services
{
    public class FriendCondition : FriendService
    {
        private InstagramEntities _context = null;
        public FriendCondition()
        {
            //Create an instance from the Database
            _context = new InstagramEntities();
        }

        // (DONE) Check if that  user Viewed is a friend or not 
        public override bool Check(int userId, int friendId)
        {
            //check user and viewed profile owner are friends from friends table
            var friend = _context.Friends.SingleOrDefault(c => c.UserId.Equals(userId) && c.FriendId.Equals(friendId));
            return friend != null;
        }
    }
}