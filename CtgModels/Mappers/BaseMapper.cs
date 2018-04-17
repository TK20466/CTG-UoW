using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtgModels.Mappers
{
    public abstract class BaseMapper
    {
        public virtual bool IncludeKey(string[] keys, string key)
        {
            return keys.Any(x => string.Equals(x, key, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
