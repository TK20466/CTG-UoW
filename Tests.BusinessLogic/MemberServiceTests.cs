using System;
using System.Collections.Generic;
using System.Linq;
using CtgBusinessLogic.Services;
using CtgModels.Enums;
using CtgModels.Interfaces.Services;
using CtgModels.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.BusinessLogic
{
    [TestClass]
    public class MemberServiceTests
    {
        private IMemberService Service => new MemberService();
        private ICostumeService CostumeService => new CostumeService();

        [TestMethod]
        public void CreateUser()
        {
            var newid = Service.CreateMember(new Member
            {
                FirstName = "Test",
                LastName = "User",
                Email = "new@new",
                ForumHandle = "HelloWorld",
                ImperialId = 1138,
                JoinDate = DateTime.UtcNow,
                Status = MemberStatus.Active
            });

            Assert.AreNotEqual(default(int), newid, "New Member Id");

            var costumeList = CostumeService.ListCostumes();

            Service.AddMemberCostume(newid, costumeList.First().Id);


        }

    }
}
