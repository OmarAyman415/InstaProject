namespace Instagram.Services
{
    public class Service : IService
    {
        public CommentService commentService;
        public ReactService reactService;
        public PostService postService;
        public UserService userService;
        public FriendService friendCondition, friendRequestedCondition;

        public Service()
        {
            commentService = new CommentService();
            reactService = new ReactService();
            postService = new PostService();
            userService = new UserService();
            friendCondition = new FriendCondition();
            friendRequestedCondition = new FriendRequestedCondition();
        }
    }
}