using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels;
using CtgModels.ServiceModels;

namespace CtgModels.Interfaces.Mappers
{
    public interface IMapper<in TEntity, out TModel> where TModel : BaseModel
    {
        TModel AsModel(TEntity entity);
        TModel AsModel(TEntity entity, params string[] includeProperties);
    }
}
