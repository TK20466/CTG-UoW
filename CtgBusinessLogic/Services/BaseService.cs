using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgDataAccess;
using CtgModels.DataModels;
using CtgModels.DataModels.Costume;
using CtgModels.Exceptions.Data;
using CtgModels.Interfaces;
using CtgModels.Mappers;
using CtgModels.ServiceModels;
using Event = CtgModels.DataModels.Event.Event;
using Member = CtgModels.DataModels.User.Member;

namespace CtgBusinessLogic.Services
{
    public abstract partial class Service : IService
    {
        public IUnitOfWork WorkUnit;

        protected Service()
        {
            WorkUnit = new UnitOfWork();
        }

        protected Service(IUnitOfWork uow)
        {
            WorkUnit = uow;
        }

        public void Dispose()
        {
            WorkUnit.Dispose();
        }

        public void SaveChanges()
        {
            WorkUnit.Save();
        }

        public IRepository<T, int> Repository<T>() where T : BaseEntity
        {
            return WorkUnit.Repository<T>();
        }
    }

    public abstract partial class Service
    {
        //Individual classes to allow the throwing of methods

        internal virtual Event GetEvent(int id)
        {
            var eventity = Repository<Event>().FindSingle(x => x.Id == id);
            if (eventity == null)
            {
                throw new DbException(DbExceptionReason.EventNotExist);
            }
            return eventity;
        }
        internal virtual Member GetMember(int id)
        {
            var member = Repository<Member>().FindSingle(x => x.Id == id);
            if (member == null)
            {
                throw new DbException(DbExceptionReason.MemberNotExist);
            }
            return member;
        }
        internal virtual LegionCostume GetCostume(int id)
        {
            var costume = Repository<LegionCostume>().FindSingle(x => x.Id == id);
            if (costume == null)
            {
                throw new DbException(DbExceptionReason.CostumeNotExist);
            }
            return costume;
        }
    }

    public abstract class Service<TEntity, TModel>
        : Service
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        public virtual TModel ServiceModel(TEntity entity)
        {
            return entity.AsModel<TModel, TEntity>();
        }
        public virtual TModel ServiceModel(TEntity entity, params string[] properties)
        {
            return entity.AsModel<TModel, TEntity>(properties);
        }
        public virtual IEnumerable<TModel> ServiceModel(IEnumerable<TEntity> entities)
        {
            return entities.AsModel<TModel, TEntity>();
        }
        public virtual IEnumerable<TModel> ServiceModel(IEnumerable<TEntity> entities, params string[] properties)
        {
            return entities.AsModel<TModel, TEntity>(properties);
        }
    }
}
