using Instagram.Models;
using Instagram.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Instagram.Services
{
    public class CommentService :ICommentService
    {
        private InstagramEntities _context = null;
        public CommentService()
        {
            //Create an instance from the Database
            _context = new InstagramEntities();
        }

        // (DONE) Add Comment to the post
        public void AddCommnet(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}