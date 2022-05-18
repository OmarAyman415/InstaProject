using Instagram.Models;
using Instagram.ViewModels;
using System.Collections.Generic;

namespace Instagram.Services
{
    public interface IReactService
    {
        void AddReact(React react);
        void UpdateReact(React react);
        React GetReact(int userId, int postId);
    }
}