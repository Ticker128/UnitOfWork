using Dapper;
using System.Data;
using UnitOfWork.Core;

namespace UnitOfWork.Sample
{
    public class CountryRepository : ICountryRepository
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public UnitOfWorkCreator UnitOfWorkCreator { get; }

        public CountryRepository(UnitOfWorkCreator unitOfWorkCreator)
        {
            UnitOfWorkCreator = unitOfWorkCreator;
            UnitOfWorkCreator.Register<CountryRepository, ICountryRepository>(this);
        }

        public object Clone()
        {
            return new CountryRepository(UnitOfWorkCreator);
        }

        public async Task Create(string name, City capital)
        {
            await Connection.ExecuteAsync("INSERT INTO COUNTRY (name,CAPITAL_ID) VALUES(@name, @cap_id)", new { name, cap_id = capital.Id });
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await Connection.QueryAsync<Country>("SELECT * FROM COUNTRY");
        }

        public async Task<Country> GetById(int id)
        {
            return await Connection.QueryFirstOrDefaultAsync<Country>("SELECT * FROM COUNTRY WHERE ID = @id", new { id });
        }
    }
}
