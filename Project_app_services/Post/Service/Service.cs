using Post.Exceptions;
using Post.Model;
using Post.Repository;

namespace Post.Service
{
    public class Service:IService
    {
        private readonly IRepo repo;
        public Service(IRepo repo)
        {
            this.repo = repo;
        }

        public void AddPost(UserPost userpost)
        {
            var up = repo.GetPostByMobileNo(userpost.Mobile_No);
            if (up == null)
            {
                repo.AddPost(userpost);
            }
            else
            {
                throw new PostAlreadyExistsException($"Post with mobile no {userpost.Mobile_No} already exists");
            }
        }

        public void DeletePost(string Mobile_No)
        {
            var up = GetPostByMobileNo(Mobile_No);
            if (up == null)
            {
                throw new PostNotFoundException($"Account with mobile no {Mobile_No} not found");
            }
            else
            {
                repo.DeletePost(Mobile_No);
            }
        }

        public List<UserPost> GetAll()
        {
            return repo.GetAll();
        }

        public UserPost GetPostByMobileNo(string Mobile_No)
        {
            var up = repo.GetPostByMobileNo(Mobile_No);
            if (up == null)
            {
                throw new PostNotFoundException($"Post with mobile no {Mobile_No} not found");
            }
            else
            {
                return repo.GetPostByMobileNo(Mobile_No);
            }
        }

        public void UpdatePost(string Mobile_No, UserPost userpost)
        {
            var up = GetPostByMobileNo(Mobile_No);
            if (up == null)
            {
                throw new PostNotFoundException($"Post with mobile no {Mobile_No} not found");
            }
            else
            {
                repo.UpdatePost(Mobile_No, userpost);
            }
        }
    }
}
