using Instagram.Models;
using System.Collections.Generic;

namespace Instagram.ViewModels
{
    public class UserFriendRequestViewModel
    {
        public User user { get; set; }
        public List<User> friends { get; set; }
    }
}