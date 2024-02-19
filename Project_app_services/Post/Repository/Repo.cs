using Post.Model;

namespace Post.Repository
{
    public class Repo : IRepo
    {
        private readonly PostContext context;
        public Repo(PostContext context)
        {
            this.context = context;
        }

        public void AddPost(UserPost userpost)
        {
            context.userpost.Add(userpost);
            context.SaveChanges();
        }

        public void DeletePost(string Mobile_No)
        {
            var up = GetPostByMobileNo(Mobile_No);
            context.userpost.Remove(up);
            context.SaveChanges();

        }

        public List<UserPost> GetAll()
        {
            return context.userpost.ToList();

        }

        public UserPost GetPostByMobileNo(string Mobile_No)
        {
            return context.userpost.Where(o => o.Mobile_No == Mobile_No).FirstOrDefault();
        }

        public void UpdatePost(string Mobile_No, UserPost userpost)
        {
            var up = GetPostByMobileNo(Mobile_No);
            up.PostId = userpost.PostId;
            up.Post = userpost.Post;
        }
    }
}
