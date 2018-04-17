using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using CtgModels.Enums;
using CtgModels.ServiceModels;

namespace ConnSquad.Security
{
    public class CTGPrincipal : IPrincipal
    {
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(MemberRankEnum rank)
        {
            return ((CTGIdentity) Identity).Ranks.Contains(rank);
        }

        public bool IsCommandTeam()
        {
            return ((CTGIdentity) Identity).Ranks.Any(x => x > 0);
        }

        public CTGPrincipal(CTGIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity { get; }
    }

    public class CTGIdentity : IIdentity
    {
        public CTGIdentity(Member member)
        {
            Member = member;
            Name = member.ImperialId.ToString();
            IsAuthenticated = true;
        }

        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }

        public Member Member { get; set; }

        public IEnumerable<MemberRankEnum> Ranks => Member.Ranks ?? new List<MemberRankEnum>();
    }
}