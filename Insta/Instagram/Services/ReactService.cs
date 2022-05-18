using Instagram.Models;
using System.Linq;

namespace Instagram.Services
{
    public class ReactService : IReactService
    {
        private InstagramEntities _context = null;
        public ReactService()
        {
            //Create an instance from the Database
            _context = new InstagramEntities();
        }

        // (DONE)React to the post
        public void AddReact(React react)
        {
            _context.Reacts.Add(react);
            _context.SaveChanges();
        }

        public void UpdateReact(React react)
        {

            react.ReactState = !react.ReactState;
            _context.SaveChanges();
        }

        // (DONE)
        public React GetReact(int userId, int postId)
        {

            return _context.Reacts.SingleOrDefault(c => c.PostId.Equals(postId) && c.UserReactedId.Equals(userId));
        }
    }
}