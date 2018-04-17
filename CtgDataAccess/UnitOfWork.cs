using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgDataAccess.db;
using CtgDataAccess.Repositories;
using CtgModels.DataModels;
using CtgModels.Interfaces;

namespace CtgDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;

        public UnitOfWork()
        {
            _context = new DataContext();
        }

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Factory method for generic repositories
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T, int> Repository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
