using ContectUs.Model;

namespace ContectUs.Service
{
    public interface IService
    {
        public List<Contect> GetAll();
        public void AddContect(Contect contect);
    }
}
