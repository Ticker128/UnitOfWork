using UnitOfWork.Core;
namespace UnitOfWork.Sample
{
    public interface ICountryRepository : IRepository
    {
        public Task<Country> GetById(int id);
        public Task<IEnumerable<Country>> GetAll();
        public Task Create(string name, City capital);
    }
}
