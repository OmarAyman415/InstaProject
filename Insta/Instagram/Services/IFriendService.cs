using Instagram.Models;
using Instagram.ViewModels;
using System;
using System.Collections.Generic;

namespace Instagram.Services
{
    public interface IFriendService
    {


        List<Friend> GetFriends(int id);

        List<User> getFriendsUsers(int id);
        bool isFriend(int userId, int friendId);

        void AddFriendUserRequest(UserAccessFriendViewModel userRequested);

        List<User> GetFriendRequests(int userId);
        bool isFriendRequested(int userId, int friendId);

        void AcceptFriendRequest(int userId, int friendId);
        void DeclineFriendRequest(int userId, int friendId);
    }
}