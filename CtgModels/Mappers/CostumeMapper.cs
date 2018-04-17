using System;
using System.Linq;
using CtgModels.DataModels.Costume;
using CtgModels.DataModels.Event;
using CtgModels.Interfaces.Mappers;
using CtgModels.ServiceModels;

namespace CtgModels.Mappers
{
    public class CostumeMapper : BaseMapper, IMapper<DataModels.Costume.LegionCostume, Costume>
    {
        public Costume AsModel(DataModels.Costume.LegionCostume entity)
        {
            return new Costume
            {
                Id = entity.Id,
                Name = entity.Name,
                Prefix = entity.Prefix
            };
        }

        public Costume AsModel(LegionCostume entity, params string[] includeProperties)
        {
            var model = AsModel(entity);

            if (this.IncludeKey(includeProperties, "Members"))
                model.Members = entity.Members.Select(x => x.AsModel<Member, DataModels.User.Member>());

            return model;
        }
    }
}