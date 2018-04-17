using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtgModels.Enums
{
    public enum MemberStatus
    {
        Recruit = 0,
        Retired = 1,
        Discharged = 2,
        Reserve = 3,
        Active = 4
    }

    public enum MemberRankEnum
    {
        Approved = 0,
        CommandingOfficer = 2,
        ExecutiveOfficer = 3,
        EventCoordinator = 4,
        MembershipLiaison = 5,
        WebLiaison = 6,
        WebMaster = 7,
        CharityRepresentative = 8,
        PublicRelations = 9
    }
}
