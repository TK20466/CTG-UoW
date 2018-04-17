using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Auth;
using CtgModels.DataModels.Costume;
using CtgModels.DataModels.Event;
using CtgModels.DataModels.Images;
using CtgModels.DataModels.User;

namespace CtgDataAccess.db
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MemberRank> Ranks { get; set; }
        public DbSet<JoinMemberRank> JoinRanks { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<ApiToken> Tokens { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<LegionCostume> Costumes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<EventParticipation> EventParticipations { get; set; }
    }
}
