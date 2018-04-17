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
using MemberEntity = CtgModels.DataModels.User.Member;

namespace CtgBusinessLogic.Services
{
    public class MemberService : Service<MemberEntity, Member>, IMemberService
    {
        public Member GetFeaturedMember()
        {
            throw new NotImplementedException();
        }

        public int CreateMember(Member memberModel)
        {
            var entity = new MemberEntity
            {
                Email = memberModel.Email,
                ForumHandle = memberModel.ForumHandle,
                JoinDate = memberModel.JoinDate,
                FirstName = memberModel.FirstName,
                LastName = memberModel.LastName,
                Status = memberModel.Status,
                LegionId = memberModel.ImperialId
            };
            Repository<MemberEntity>().Insert(entity);
            SaveChanges();
            return entity.Id;
        }

        public void DeleteMember(int memberId)
        {
            var repo = Repository<MemberEntity>();
            var entity = repo.FindSingle(x => x.Id == memberId);
            repo.Delete(entity);

            SaveChanges();
        }

        public void AddMemberCostume(int memberId, int costumeId)
        {
            var costume = Repository<LegionCostume>().FindSingle(x => x.Id == costumeId);
            var member = Repository<MemberEntity>().FindSingle(x => x.Id == memberId);
            member.Costumes = member.Costumes ?? new List<LegionCostume>();
            member.Costumes.Add(costume);

            SaveChanges();
        }

        public void RemoveMemberCostume(int memberId, int costumeId)
        {
            var member = Repository<MemberEntity>().FindSingle(x => x.Id == memberId);
            var costume = member.Costumes.FirstOrDefault(x => x.Id == costumeId);
            if (costume != null)
                member.Costumes.Remove(costume);
            SaveChanges();
        }

        public IEnumerable<Member> GetMembers()
        {
            return Repository<MemberEntity>()
                .Select()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .MakeConcrete()
                .Select(this.ServiceModel);

        }

        public void UpdateMember(Member memberModel)
        {
            var repo = Repository<MemberEntity>();
            var userEntity = repo.FindSingle(x => x.Id == memberModel.Id);
            userEntity.Email = memberModel.Email;
            userEntity.FirstName = memberModel.FirstName;
            userEntity.LastName = memberModel.LastName;
            userEntity.ForumHandle = memberModel.ForumHandle;
            userEntity.Status = memberModel.Status;
            userEntity.LegionId = memberModel.ImperialId;

            SaveChanges();
        }
    }
}
