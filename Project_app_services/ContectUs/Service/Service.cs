using ContectUs.Model;
using ContectUs.Repository;

namespace ContectUs.Service
{
    public class Service:IService
    {
        private readonly IRepo repo;
        public Service(IRepo repo)
        {
            this.repo = repo;
        }

        public void AddContect(Contect contect)
        {
            repo.AddContect(contect);
        }

        public List<Contect> GetAll()
        {
            return repo.GetAll();
        }
    }
}
