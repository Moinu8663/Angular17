using ContectUs.Model;

namespace ContectUs.Repository
{
    public interface IRepo
    {
        public List<Contect> GetAll();
        public void AddContect(Contect contect);
    }
}
