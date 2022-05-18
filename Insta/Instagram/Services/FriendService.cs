using Instagram.Models;
using Instagram.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Instagram.Services
{
    public abstract class FriendService
    {
        private InstagramEntities _context = null;
        public FriendService()
        {
            //Create an instance from the Database
            _context = new InstagramEntities();
        }

        //(DONE) Get all User's friends but as friend Model  
        //Which Means get their IDs only
        public List<Friend> GetFriends(int id)
        {
            var friends = _context.Friends.Where(m => m.UserId.Equals(id)).ToList();
            return friends;
        }

        // (DONE) Get all User's friends but as user Model 
        public List<User> getFriendsUsers(int id)
        {
            //Get Friends IDs 
            var friends = _context.Friends.Where(m => m.UserId.Equals(id)).ToList();

            List<User> FriendsUser = new List<User>();
            foreach (var friend in friends)//Add Each friend his data from Database
            {
                FriendsUser.Add(_context.Users.SingleOrDefault(m => m.UserId.Equals(friend.FriendId)));
            }
            return FriendsUser;
        }

        public abstract bool Check(int userId, int friendId);


        // (DONE) Check if that  user Viewed is a friend or not 
        // public bool isFriend(int userId, int friendId)
        // {

        //     //check user and viewed profile owner are friends from friends table
        //     var friend = _context.Friends.SingleOrDefault(c => c.UserId.Equals(userId) && c.FriendId.Equals(friendId));
        //     return friend != null;
        // }

        // // (DONE) Check if that  user sent friend request or not to viewed profile s
        // public bool isFriendRequested(int userId, int friendId)
        // {

        //     var friend = _context.FriendRequests.SingleOrDefault(c => c.UserId.Equals(userId) && c.FriendId.Equals(friendId));
        //     return friend != null;
        // }

        // (DONE) Create friend request 
        public void AddFriendUserRequest(UserAccessFriendViewModel userRequested)
        {

            int userID = userRequested.user.UserId;
            int friendID = userRequested.userFriend.UserId;

            //Create request record
            FriendRequest request = new FriendRequest()
            {
                UserId = userID,
                FriendId = friendID
            };

            //Add record to FriendRequest Table
            _context.FriendRequests.Add(request);
            //Save changes to Database
            _context.SaveChanges();

        }


        // (DONE) Get All Friend Requests Sent to the User 
        public List<User> GetFriendRequests(int userId)
        {

            //Get Friend requests from FriendRequest table
            var Requests = _context.FriendRequests.Where(c => c.FriendId.Equals(userId)).ToList();

            List<User> friendsRequests = new List<User>();

            //Get Friends Data
            foreach (var request in Requests)
            {
                //Get Friend Data
                var friendRequestData = _context.Users.SingleOrDefault(c => c.UserId.Equals(request.UserId));

                //Add friend to FriendRequests List
                friendsRequests.Add(friendRequestData);
            }
            return friendsRequests;
        }

        // (DONE) Remove friend Request 
        private void RemoveFriendRequestFromDatabase(int userId, int friendId)
        {

            // Get Friend Request Record(s)
            FriendRequest friendRequest = _context.FriendRequests.SingleOrDefault(c => c.FriendId.Equals(userId) && c.UserId.Equals(friendId));

            // Remove Friend request Record from database
            _context.FriendRequests.Remove(friendRequest);
            _context.SaveChanges();
        }

        // (DONE)
        private void AddFriendToDatabase(int userId, int friendId)
        {


            // Create Friend record , As two users each has a record that he is a friend to the other user,
            // Which Means two users friends have two record in Friends table

            Friend user1 = new Friend()//First record
            {
                UserId = userId,
                FriendId = friendId,
            };

            Friend user2 = new Friend()// Second Record
            {
                UserId = friendId,
                FriendId = userId,
            };

            //Add records to the database
            _context.Friends.Add(user1);
            _context.Friends.Add(user2);

            //Save changes to Database
            _context.SaveChanges();
        }

        // (DONE)
        public void AcceptFriendRequest(int userId, int friendId)
        {

            RemoveFriendRequestFromDatabase(userId, friendId);

            AddFriendToDatabase(userId, friendId);
        }

        // (DONE)
        public void DeclineFriendRequest(int userId, int friendId)
        {

            RemoveFriendRequestFromDatabase(userId, friendId);
        }
    }
}