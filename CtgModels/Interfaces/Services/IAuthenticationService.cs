using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.Enums;

namespace CtgModels.Interfaces.Services
{
    public interface IAuthenticationService
    {
        bool UserHasRank(int accountId, MemberRankEnum rank);
        void ClearTokens(int accountId);
        int CreateCredentials(string username, string password, string email);
        int CreateCredentials(string username, string password, string email, int tkid, string firstName, string lastName);

        void DeleteCredentials(string username);

        int ValidateCredentials(string username, string password);
        Guid GetAPIToken(int accountId);
        ServiceModels.Member ValidateAPIToken(Guid token);

        Guid CreateVerificationCode(int accountId);

        bool VerifyEmailVerificationCode(Guid code);
    }
}
