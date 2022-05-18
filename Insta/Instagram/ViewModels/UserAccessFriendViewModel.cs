using Instagram.Models;
using System.Collections.Generic;

namespace Instagram.ViewModels
{
    public class UserAccessFriendViewModel
    {


        public User user { get; set; }

        public bool isFriend { get; set; }
        public User userFriend { get; set; }
        public List<Friend> friends { get; set; }

        public bool isFriendRequested { get; set; }


        public UserAccessFriendViewModel(User user, User userFriend, List<Friend> friends, bool isFriend, bool isFriendRequested)
        {
            this.user = user;
            this.userFriend = userFriend;
            this.friends = friends;
            this.isFriend = isFriend;
            this.isFriendRequested = isFriendRequested;
        }

    }
}