using System.Collections.Generic;

namespace CtgModels.ServiceModels
{
    public class Costume : BaseModel
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}