using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.Enums;

namespace CtgModels.ServiceModels
{

    public class Member : BaseModel
    {
        public int ImperialId { get; set; }
        public string LegionUrl { get; set; }
        public DateTime JoinDate { get; set; }
        public IEnumerable<MemberImage> Images { get; set; }
        public MemberImage ProfileImage { get; set; }
        public IEnumerable<Event> Events { get; set; } 
        public string Biography { get; set; }

        public bool Featured { get; set; }
        public string Email { get; set; }
        public string ForumHandle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MemberStatus Status { get; set; }
        public IEnumerable<Costume> Costumes { get; set; }

        public IEnumerable<MemberRankEnum> Ranks { get; set; }
    }

    public class MemberImage : BaseModel
    {
        public string Path { get; set; }
        public string Caption { get; set; }
    }
}
