using System.Data;

namespace UnitOfWork.Core
{
    internal class UnitOfWork : IUnitOfWork
    {
        public Dictionary<string, IRepository> Repositories { get; }

        private IDbTransaction transaction;
        private readonly IDbConnection connection;
        private readonly IsolationLevel IsolationLevel;

        public UnitOfWork(Dictionary<string, IRepository> repos, IDbConnection conn, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            IsolationLevel = isolationLevel;
            Repositories = repos;
            connection = conn;
            connection.Open();
            transaction = connection.BeginTransaction(IsolationLevel);
        }

        public T Get<T>() where T: IRepository
        {
            var repos = (T)Repositories[typeof(T).Name];
            repos.Connection = connection;
            repos.Transaction = transaction;
            return repos;
        }

        public void Commit()
        {
            transaction.Commit();
            transaction.Dispose();
            transaction = connection.BeginTransaction(IsolationLevel);
        }
        
        public void Rollback()
        {
            transaction.Rollback();
            transaction.Dispose();
            transaction = connection.BeginTransaction(IsolationLevel);
        }

        public void Dispose()
        {
            transaction.Connection?.Close();
            transaction.Connection?.Dispose();
            transaction.Dispose();
        }
    }
}
