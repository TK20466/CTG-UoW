using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.ServiceModels;

namespace CtgModels.Interfaces.Services
{
    public interface ICostumeService
    {
        IEnumerable<Costume> ListCostumes();
        int CreateCostume(string name, string prefix);
        int CreateCostume(Costume model);
        void DeleteCostume(int costumeId);
    }
}
