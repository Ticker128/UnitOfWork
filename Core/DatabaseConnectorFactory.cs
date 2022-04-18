using System.Data;

namespace UnitOfWork.Core
{
    public abstract class DatabaseConnectorFactory
    {
        public abstract string ConnectionString { get; }
        public abstract IDbConnection GetConnection();
    }
}
