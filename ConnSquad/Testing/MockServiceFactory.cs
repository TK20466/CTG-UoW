using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CtgModels.Enums;
using CtgModels.Exceptions.Tokens;
using CtgModels.Interfaces;
using CtgModels.Interfaces.Services;
using CtgModels.ServiceModels;
using Moq;

namespace ConnSquad.Testing
{
    public class MockServiceFactory : IServiceFactory
    {
        public IEventsService CreateEventsService()
        {
            var mock = new Mock<IEventsService>();
            mock.Setup(x => x.FutureEvents(It.IsAny<int>())).Returns<int>(x =>
            {
                var list = new List<Event>();
                for (var i = 0; i < x; i++)
                {
                    list.Add(new Event()
                    {
                        Date = DateTime.Now.AddDays(i * 7),
                        Address = "Location " + i,
                        Description = "Event for the cause of item " + i,
                        Title = "Title for Event at " + i
                    });
                }
                return list;
            });

            return mock.Object;
        }

        public IAuthenticationService CreateAuthenticationService()
        {
            var mock = new Mock<IAuthenticationService>();
            mock.Setup(x => x.ValidateCredentials("user", "password")).Returns(1);
            mock.Setup(x => x.ValidateCredentials("user", "wrongPassword")).Throws<UserPasswordWrongException>();
            mock.Setup(x => x.GetAPIToken(1)).Returns(Guid.NewGuid);
            mock.Setup(x => x.ValidateAPIToken(It.IsAny<Guid>())).Returns(new Member
            {
                Status = MemberStatus.Active,
                FirstName = "Sarah",
                LastName = "Bailey",
                Email = "sarah.katherine.bailey@gmail.com",
                ForumHandle = "Alay",
                Ranks = new List<MemberRankEnum>()
                {
                    MemberRankEnum.Approved,
                    MemberRankEnum.MembershipLiaison,
                    MemberRankEnum.WebLiaison,
                    MemberRankEnum.WebMaster
                }
            });
            return mock.Object;
        }

        public IMemberService CreateMemberService()
        {
            var mock = new Mock<IMemberService>();
            mock.Setup(x => x.GetFeaturedMember()).Returns(new Member
            {
                FirstName = "Dennis",
                LastName = "Vala",
                Featured = true,
                Id = 1,
                ImperialId = 11943,
                JoinDate = new DateTime(2017, 1, 28),
                LegionUrl = "http://www.501st.com/members/displaymemberdetails.php?userID=23507",
                ProfileImage = new MemberImage
                {
                    Caption = "Profile Picture",
                    Path = "/Content/images/members/11943.png"
                }
            });
            return mock.Object;
        }
    }
}