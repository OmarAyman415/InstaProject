using Instagram.Models;
using System.Linq;

namespace Instagram.Services
{
    public class PostService : IPostService
    {
        private InstagramEntities _context = new InstagramEntities();
        public PostService()
        {
            //Create an instance from the Database
            _context = new InstagramEntities();
        }

        // (DONE)
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);

            //Save changes to Database
            _context.SaveChanges();
        }


        // (DONE) Get Post Data from Database
        public Post GetPost(int postId)
        {

            Post post = _context.Posts.SingleOrDefault(c => c.PostId.Equals(postId));
            return post;
        }


        public void IncrementPostReact(int postId, bool ReactState)
        {

            Post postData = _context.Posts.SingleOrDefault(c => c.PostId.Equals(postId));
            if (ReactState)
            {
                postData.Likes++;
            }
            else
            {
                postData.Deslikes++;
            }

            _context.SaveChanges();
        }


        // (DONE)
        public void DecrementLikes(Post post)
        {
            if (post.Likes > 0)
            {
                post.Likes--;
            }
        }

        // (DONE)
        public void DecrementDisLikes(Post post)
        {
            if (post.Deslikes > 0)
            {
                post.Deslikes--;
            }
        }

        // (DONE)
        public void DecrementPostReact(int postId, bool ReactState)
        {

            Post postData = _context.Posts.SingleOrDefault(c => c.PostId.Equals(postId));
            if (ReactState)
            {
                DecrementLikes(postData);
            }
            else
            {
                DecrementDisLikes(postData);
            }

            _context.SaveChanges();
        }
    }
}