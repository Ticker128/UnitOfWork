using UnitOfWork.Core;
using UnitOfWork.Sample;

Npgsql.NpgsqlConnectionStringBuilder dbStringBuilder = new()
{
    Username = "postgres",
    Password = "password",
    Database = "postgres",
    Host = "localhost",
    Port = 5432,
    IntegratedSecurity = false,
    Pooling = true
};
var dbFactory = new PostgresDatabaseConnectorFactory(dbStringBuilder.ConnectionString);
var unitFactory = new UnitOfWorkCreator(dbFactory);
var citiesrep = new CityRepository(unitFactory);
var countri = new CountryRepository(unitFactory);
IEnumerable<Country> res;
using (IUnitOfWork unitOfWork = unitFactory.GetUnitOfWorkOwnRepository())
{
    res = await unitOfWork.Get<ICountryRepository>().GetAll();
}
foreach (var item in res)
{
    Console.WriteLine($"{item.Id} {item.Name} {item.Capital_ID}");
}
