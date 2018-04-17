using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgDataAccess.db;
using CtgModels.DataModels;

namespace CtgDataAccess.Repositories
{
    public class GenericRepository<T> : Repository<T, int> where T : BaseEntity
    {

        public GenericRepository() : base()
        { }

        public GenericRepository(DataContext context) : base(context)
        { }

        public override T FetchByKey(int value)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == value);
        }
    }
}
