using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.User;

namespace CtgModels.DataModels.Costume
{
    public class LegionCostume : BaseEntity
    {
        public string Name { get; set; }
        public string Prefix { get; set; }

        /// <summary>
        /// Members that have this costume
        /// </summary>
        public virtual ICollection<Member> Members { get; set; }

        /// <summary>
        /// Events where members used this costume
        /// </summary>
        public virtual ICollection<EventParticipation> Events { get; set; }
    }
}
