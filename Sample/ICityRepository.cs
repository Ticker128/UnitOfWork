using UnitOfWork.Core;
namespace UnitOfWork.Sample
{
    public interface ICityRepository: IRepository
    {
        public Task<City> GetById(int id);
        public Task<IEnumerable<City>> GetAll();
        public Task Create(string name);
    }
}
