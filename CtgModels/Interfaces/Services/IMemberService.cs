using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.ServiceModels;

namespace CtgModels.Interfaces.Services
{
    public interface IMemberService
    {
        Member GetFeaturedMember();

        int CreateMember(Member memberModel);
        void DeleteMember(int memberId);
        void AddMemberCostume(int memberId, int costumeId);
        void RemoveMemberCostume(int memberId, int costumeId);
        IEnumerable<Member> GetMembers();
        void UpdateMember(Member memberModel);
    }
}
