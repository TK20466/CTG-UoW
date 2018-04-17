using System.Linq;
using CtgModels.Interfaces.Mappers;
using CtgModels.ServiceModels;

namespace CtgModels.Mappers
{
    public class EventMapper : BaseMapper, IMapper<DataModels.Event.Event, ServiceModels.Event>
    {
        public Event AsModel(DataModels.Event.Event entity)
        {
            return new Event
            {
                Date = entity.Date,
                EndDate = entity.EndDate,
                Display = entity.Display,
                WeaponsAllowed = entity.WeaponsAllowed,
                Status = entity.Status,
                Description = entity.Description,
                Address = entity.Address,
                State = entity.State,
                LFLApproval = entity.LFLApproval,
                Wesbite = entity.Website,
                Town = entity.Town,
                ZipCode = entity.ZipCode,
                Title = entity.Name,
                Id = entity.Id
            };
        }

        public Event AsModel(DataModels.Event.Event entity, params string[] includeProperties)
        {
            var model = AsModel(entity);

            if (this.IncludeKey(includeProperties, "EventImage"))
                model.FeaturedImage = entity.EventImage.AsModel<Image, DataModels.Images.Image>();

            if (this.IncludeKey(includeProperties, "Requestor"))
                model.Requestor = entity.Requestor.AsModel<Contact, DataModels.Event.Contact>();

            if (this.IncludeKey(includeProperties, "Participants"))
                model.Participants = entity.Participants.Select(x => new EventParticipation { MemberId = x.MemberId, CostumeId = x.CostumeId});


            return model;
        }
    }
}