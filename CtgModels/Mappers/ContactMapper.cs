using System.Linq;
using CtgModels.Interfaces.Mappers;
using CtgModels.ServiceModels;
using Event = CtgModels.DataModels.Event.Event;

namespace CtgModels.Mappers
{
    public class ContactMapper : BaseMapper, IMapper<DataModels.Event.Contact, ServiceModels.Contact>
    {
        public ServiceModels.Contact AsModel(DataModels.Event.Contact entity)
        {
            return new ServiceModels.Contact
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.LastName,
                Id = entity.Id
            };
        }

        public Contact AsModel(DataModels.Event.Contact entity, params string[] includeProperties)
        {
            var model = AsModel(entity);

            if (this.IncludeKey(includeProperties, "Events"))
                model.Events = entity.Events.Select(x => x.AsModel<ServiceModels.Event, Event>());

            return model;
        }
    }
}