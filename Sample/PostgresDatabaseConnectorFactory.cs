using System.Data;
using Npgsql;
using UnitOfWork.Core;

namespace UnitOfWork.Sample
{
    public class PostgresDatabaseConnectorFactory : DatabaseConnectorFactory
    {
        public override string ConnectionString { get; }
        public PostgresDatabaseConnectorFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public override IDbConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }
    }
}
