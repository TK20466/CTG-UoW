using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Costume;
using CtgModels.DataModels.Images;
using CtgModels.Enums;

namespace CtgModels.DataModels.User
{
    public class Member : BaseEntity
    {
        public int LegionId { get; set; }

        public string ForumHandle { get; set; }
        public string Email { get; set; }

        public DateTime? JoinDate { get; set; }
        public virtual ICollection<JoinMemberRank> Ranks { get; set; }
        public MemberStatus Status { get; set; }
        public virtual ICollection<LegionCostume> Costumes { get; set; }
        public virtual ICollection<EventParticipation> Events { get; set; }
        public virtual Image FeatureImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class MemberRank : BaseEntity
    {
        public MemberRankEnum Rank { get; set; }
        public virtual ICollection<JoinMemberRank> Members { get; set; }
    }

    public class JoinMemberRank : BaseEntity
    {
        public virtual Member Member { get; set; }
        public virtual MemberRank Rank { get; set; }
    }

    public class EventParticipation : BaseEntity
    {
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public virtual Event.Event Event { get; set; }

        /// <summary>
        /// If null, they're wrangling
        /// </summary>
        [ForeignKey("Costume")]
        public int? CostumeId { get; set; }
        public virtual LegionCostume Costume { get; set; }

        /// <summary>
        /// So did they actually show up?
        /// </summary>
        public bool Participated { get; set; }
    }
}
