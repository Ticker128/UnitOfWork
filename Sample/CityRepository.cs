using Dapper;
using System.Data;
using UnitOfWork.Core;

namespace UnitOfWork.Sample
{
    public class CityRepository : ICityRepository
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public UnitOfWorkCreator UnitOfWorkCreator { get; }
        public CityRepository(UnitOfWorkCreator unitOfWorkCreator)
        {
            UnitOfWorkCreator = unitOfWorkCreator;
            UnitOfWorkCreator.Register<CityRepository, ICityRepository>(this);
        }

        public async Task Create(string name)
        {
            await Connection.ExecuteAsync("INSERT INTO CITY (name) VALUES(@name)", new { name });
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await Connection.QueryAsync<City>("SELECT * FROM CITY");
        }

        public async Task<City> GetById(int id)
        {
            return await Connection.QueryFirstOrDefaultAsync<City>("SELECT * FROM CITY WHERE ID = @id", new { id });
        }
        public object Clone()
        {
            return new CityRepository(UnitOfWorkCreator);
        }
    }
}
