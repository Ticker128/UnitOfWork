namespace UnitOfWork.Core
{
    public class UnitOfWorkCreator
    {
        private readonly DatabaseConnectorFactory databaseConnectorFactory;
        private readonly Dictionary<string, IRepository> repositories = new();
        public UnitOfWorkCreator(DatabaseConnectorFactory databaseConnector)
        {
            databaseConnectorFactory = databaseConnector;
        }
        public void Register<T, U>(T repos) where T : IRepository
        {
            repositories.TryAdd(typeof(U).Name, repos);
        }
        public IUnitOfWork GetUnitOfWork(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            return new UnitOfWork(repositories, databaseConnectorFactory.GetConnection(), isolationLevel);
        }
        public IUnitOfWork GetUnitOfWorkOwnRepository(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            Dictionary<string, IRepository> repos = new();
            foreach (var item in repositories)
            {
                repos.Add(item.Key, (IRepository)item.Value.Clone());
            }
            return new UnitOfWork(repos, databaseConnectorFactory.GetConnection(), isolationLevel);
        }
    }
}
