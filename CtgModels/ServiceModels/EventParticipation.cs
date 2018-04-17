using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtgModels.ServiceModels
{
    public class EventParticipation : BaseModel
    {
        private new int Id { get; set; }
        public int MemberId { get; set; }
        public int? CostumeId { get; set; }
    }
}
