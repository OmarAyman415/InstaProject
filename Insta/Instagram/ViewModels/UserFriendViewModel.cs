using Instagram.Models;
using System.Collections.Generic;

namespace Instagram.ViewModels
{
    public class UserFriendViewModel
    {
        public User user { get; set; }
        public List<Friend> friends { get; set; }


    }
}