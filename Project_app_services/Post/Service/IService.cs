using Post.Model;

namespace Post.Service
{
    public interface IService
    {
        public List<UserPost> GetAll();
        public UserPost GetPostByMobileNo(string Mobile_No);
        public void AddPost(UserPost userpost);
        public void UpdatePost(string Mobile_No, UserPost userpost);
        public void DeletePost(string Mobile_No);
    }
}
