using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Costume;
using CtgModels.Interfaces.Mappers;
using CtgModels.ServiceModels;
using Contact = CtgModels.DataModels.Event.Contact;
using Event = CtgModels.DataModels.Event.Event;
using Image = CtgModels.DataModels.Images.Image;
using Member = CtgModels.DataModels.User.Member;

namespace CtgModels.Mappers
{
    public static class Mappers
    {
        //public static ServiceModels.Member AsModel(this DataModels.User.Member entity)
        //{
        //    return new MemberMapper().AsModel(entity);
        //}
        //public static ServiceModels.Costume AsModel(this DataModels.Costume.LegionCostume entity)
        //{
        //    return new CostumeMapper().AsModel(entity);
        //}
        //public static IEnumerable<ServiceModels.Costume> AsModel(this IEnumerable<DataModels.Costume.LegionCostume> entites)
        //{
        //    return entites.Select(x => x.AsModel());
        //}
        //public static ServiceModels.Event AsModel(this DataModels.Event.Event entity)
        //{
        //    return new EventMapper().AsModel(entity);
        //}
        //public static IEnumerable<ServiceModels.Event> AsModel(this IEnumerable<DataModels.Event.Event> entites)
        //{
        //    return entites.Select(x => x.AsModel());
        //}
        //public static ServiceModels.Image AsModel(this Image entity)
        //{
        //    return new ImageMapper().AsModel(entity);
        //}
        //public static IEnumerable<ServiceModels.Image> AsModel(this IEnumerable<Image> entites)
        //{
        //    return entites.Select(x => x.AsModel());
        //}
        //public static ServiceModels.Contact AsModel(this Contact entity)
        //{
        //    return new ContactMapper().AsModel(entity);
        //}
        //public static IEnumerable<ServiceModels.Contact> AsModel(this IEnumerable<Contact> entites)
        //{
        //    return entites.Select(x => x.AsModel());
        //}

        public static TOut AsModel<TOut, T>(this T entity) where TOut : BaseModel
        {
            var mapper = GetMapper<T, TOut>();
            return mapper.AsModel(entity);
        }
        public static TOut AsModel<TOut, T>(this T entity, params string[] includeProperties) where TOut : BaseModel
        {
            var mapper = GetMapper<T, TOut>();
            return mapper.AsModel(entity, includeProperties);
        }
        public static IEnumerable<TOut> AsModel<TOut, T>(this IEnumerable<T> entities) where TOut : BaseModel
        {
            var mapper = GetMapper<T, TOut>();
            return entities.Select(x => mapper.AsModel(x));
        }
        public static IEnumerable<TOut> AsModel<TOut, T>(this IEnumerable<T> entities, params string[] includeProperties) where TOut : BaseModel
        {
            var mapper = GetMapper<T, TOut>();
            return entities.Select(x => mapper.AsModel(x, includeProperties));
        }

        private static IMapper<T, TOut> GetMapper<T, TOut>() where TOut : BaseModel
        {
            return MapperTypes[typeof(T)] as IMapper<T, TOut>;
        }

        private static readonly IDictionary<Type, object> MapperTypes = new Dictionary<Type, object>()
        {
            { typeof(Member), new MemberMapper() },
            { typeof(LegionCostume), new CostumeMapper() },
            { typeof(Event), new EventMapper() },
            { typeof(Image), new ImageMapper() },
            { typeof(Contact), new ContactMapper() },
        };
    }


}
