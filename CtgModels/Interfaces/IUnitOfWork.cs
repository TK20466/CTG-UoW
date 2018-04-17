using System;
using CtgModels.DataModels;

namespace CtgModels.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        IRepository<T, int> Repository<T>() where T : BaseEntity;
    }
}