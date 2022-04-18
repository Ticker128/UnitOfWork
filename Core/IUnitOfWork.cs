using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitOfWork.Core
{
    public interface IUnitOfWork: IDisposable
    {
        public void Commit();
        public void Rollback();
        public T Get<T>() where T : IRepository;
    }
}
