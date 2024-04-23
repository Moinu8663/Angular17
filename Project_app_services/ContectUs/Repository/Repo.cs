using ContectUs.Model;

namespace ContectUs.Repository
{
    public class Repo:IRepo
    {
        private readonly ContectContext context;
        public Repo(ContectContext context)
        {
            this.context = context;
        }

        public void AddContect(Contect contect)
        {
            context.contects.Add(contect);
            context.SaveChanges();
        }

        public List<Contect> GetAll()
        {
            return context.contects.ToList();
        }
    }
}
