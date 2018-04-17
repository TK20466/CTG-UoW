using CtgModels.Interfaces.Mappers;
using CtgModels.ServiceModels;

namespace CtgModels.Mappers
{
    public class ImageMapper : BaseMapper, IMapper<DataModels.Images.Image, ServiceModels.Image>
    {
        public ServiceModels.Image AsModel(DataModels.Images.Image entity)
        {
            return new ServiceModels.Image
            {
                Url = entity.Url,
                Format = entity.Format,
                ImageType = entity.ImageType,
                Id = entity.Id
            };
        }

        public Image AsModel(DataModels.Images.Image entity, params string[] includeProperties)
        {
            return AsModel(entity);
        }
    }
}