using System;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels;

namespace CtgModels.Interfaces
{
    public interface IService : IDisposable
    {
        void SaveChanges();
        IRepository<T, int> Repository<T>() where T : BaseEntity;
    }
}
