using System;
using CtgBusinessLogic.Services;
using CtgModels.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.BusinessLogic
{
    [TestClass]
    public class AuthenticationServiceTests
    {

        private IAuthenticationService _service;
        private IAuthenticationService Service => _service ?? (_service = new AuthenticationService());

        private const string testUserName = "sarahRoxxxx";
        private const string testPassword = "sarahIsAwesome";
        private const string testEmail = "sarahIsAmazing@gmail.com";

        [TestMethod]
        public void TestCreateUser()
        {
            var accountId = Service.CreateCredentials(testUserName, testPassword, testEmail);
            Assert.AreNotEqual(default(int), accountId, "Account Id");

            var verificationCode = Service.CreateVerificationCode(accountId);
            Assert.AreNotEqual(Guid.Empty, verificationCode, "Verification Code");

            var verified = Service.VerifyEmailVerificationCode(verificationCode);
            Assert.IsTrue(verified, "Verification Code Validation");

            var login = Service.ValidateCredentials(testUserName, testPassword);
            Assert.IsTrue(login != default(int), "Login");

            var APIToken = Service.GetAPIToken(accountId);
            Assert.AreNotEqual(Guid.Empty, APIToken, "API Token");

            var valid = Service.ValidateAPIToken(APIToken);
            Assert.IsTrue(valid != null, "API Token Test");
        }

        [TestMethod]
        public void DeleteTestUser()
        {
            Service.DeleteCredentials(testUserName);
        }
    }
}
