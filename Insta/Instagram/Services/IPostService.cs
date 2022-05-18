using Instagram.Models;
using Instagram.ViewModels;
using System.Collections.Generic;

namespace Instagram.Services
{
    public interface IPostService
    {
        Post GetPost(int postId);

        void AddPost(Post post);

        void DecrementLikes(Post post);

        void DecrementDisLikes(Post post);

        void DecrementPostReact(int postId, bool ReactState);

        void IncrementPostReact(int postId, bool ReactState);

    }
}