using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Costume;
using CtgModels.Extensions;
using CtgModels.Interfaces.Services;
using CtgModels.Mappers;
using CtgModels.ServiceModels;

namespace CtgBusinessLogic.Services
{
    public class CostumeService : Service<LegionCostume, Costume>, ICostumeService
    {
        public IEnumerable<Costume> ListCostumes()
        {
            return
                Repository<LegionCostume>()
                .Select()
                .OrderBy(x => x.Prefix)
                .ThenBy(x => x.Name)
                .Execute().Select(this.ServiceModel);
        }

        public int CreateCostume(string name, string prefix)
        {
            var entity = new LegionCostume
            {
                Name = name,
                Prefix = prefix
            };
            Repository<LegionCostume>().Insert(entity);
            SaveChanges();
            return entity.Id;
        }
        public int CreateCostume(Costume model)
        {
            return CreateCostume(model.Name, model.Prefix);
        }

        public void DeleteCostume(int costumeId)
        {
            var repo = Repository<LegionCostume>();
            var entity = repo.FindSingle(x => x.Id == costumeId);
            repo.Delete(entity);
            SaveChanges();
        }
    }
}
