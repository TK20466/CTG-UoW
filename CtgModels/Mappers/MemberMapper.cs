using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Costume;
using CtgModels.DataModels.User;
using CtgModels.Interfaces.Mappers;
using CtgModels.ServiceModels;
using Member = CtgModels.ServiceModels.Member;

namespace CtgModels.Mappers
{
    public class MemberMapper : BaseMapper, IMapper<DataModels.User.Member, ServiceModels.Member>
    {
        public ServiceModels.Member AsModel(DataModels.User.Member entity)
        {
            return new ServiceModels.Member
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                ImperialId = entity.LegionId,
                ForumHandle = entity.ForumHandle,
                LastName = entity.LastName,
                Email = entity.Email,   
                Status = entity.Status,
                Ranks = entity.Ranks?.Select(x => x.Rank.Rank)
            };
        }

        public Member AsModel(DataModels.User.Member entity, params string[] includeProperties)
        {
            var model = AsModel(entity);

            if (this.IncludeKey(includeProperties, "Events"))
                model.Events = entity.Events.Select(x => x.Event.AsModel<Event, DataModels.Event.Event>());
            if (this.IncludeKey(includeProperties, "Costumes"))
                model.Costumes = entity.Costumes.AsModel<Costume, LegionCostume>();

            return model;
        }
    }

    public enum MemberMapperEnum
    {
        Default=0,
        IncludeEvents=1
    }
}
