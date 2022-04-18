using System.Data;
namespace UnitOfWork.Core
{
    public interface IRepository: ICloneable
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public UnitOfWorkCreator UnitOfWorkCreator { get; }
    }
}
