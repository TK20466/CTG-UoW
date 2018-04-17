using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgBusinessLogic.Security;
using CtgModels.DataModels.Auth;
using CtgModels.DataModels.User;
using CtgModels.Enums;
using CtgModels.Exceptions.Tokens;
using CtgModels.Interfaces;
using CtgModels.Interfaces.Services;

namespace CtgBusinessLogic.Services
{
    public class AuthenticationService : Service<Member, CtgModels.ServiceModels.Member>, IAuthenticationService
    {

        public bool UserHasRank(int accountId, MemberRankEnum rank)
        {
            var user = Repository<Account>().FindSingle(x => x.Id == accountId);
            var result = user.Details.Ranks?.Any(x => x.Rank.Rank == rank);
            return result ?? false;
        }

        public void ClearTokens(int accountId)
        {
            var repository = Repository<ApiToken>();

            var tokens = repository.FindBy(x => x.UserId == accountId);
            repository.Delete(tokens);

            SaveChanges();
        }

        public int ValidateCredentials(string username, string password)
        {
            var repository = Repository<Account>();
            var user = repository.FindSingle(x => x.UserName == username);

            if (user == null) throw new UserDoesntExistException();

            if (!user.Verified) throw new UserNotVerifiedException();

            if (PasswordStorage.VerifyPassword(password, user.Password)) return user.Id;
            throw new UserPasswordWrongException();
        }



        public Guid GetAPIToken(int accountId)
        {
            var token = new ApiToken
            {
                UserId = accountId,
                CreationDate = DateTime.UtcNow,
                Token = Guid.NewGuid()
            };
            Repository<ApiToken>().Insert(token);
            SaveChanges();

            return token.Token;
        }

        public CtgModels.ServiceModels.Member ValidateAPIToken(Guid token)
        {
            const int tokenDays = 7;
            var repository = Repository<ApiToken>();
            var entity = repository.FindSingle(x => x.Token == token);

            if (entity == null) return null;
            if (entity.CreationDate.AddDays(tokenDays) < DateTime.UtcNow) return null;
            return ServiceModel(entity.User.Details);

        }

        public Guid CreateVerificationCode(int accountId)
        {
            var emailGuid = Guid.NewGuid();
            var account = Repository<Account>().FindSingle(x => x.Id == accountId);
            if (account.Verification == null)
                account.Verification = new Verification();
            account.Verification.Code = emailGuid;
            account.Verified = false;

            SaveChanges();
            return emailGuid;
        }

        public bool VerifyEmailVerificationCode(Guid code)
        {
            var repository = Repository<Verification>();
            var verification = repository.FindSingle(x => x.Code == code);
            if (verification == null) return false;
            verification.Account.Verified = true;
            repository.Delete(verification);
            SaveChanges();

            return true;
        }

        private Member FindOrCreateMember(string email, int tkid, string firstName, string lastName)
        {
            var repo = Repository<Member>();
            var entity = repo.FindSingle(x => (x.FirstName == firstName && x.LastName == lastName) || x.LegionId == tkid);
            if (entity == null)
                entity = new Member()
                {
                    LegionId = tkid,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };
            return entity;
        }

        public int CreateCredentials(string username, string password, string email, int tkid, string firstName, string lastName)
        {
            var repo = Repository<Account>();
            if (repo.FindSingle(x => x.UserName == username) != null)
                throw new UsernameInUseException();

            var hashedPassword = PasswordStorage.CreateHash(password);

            var account = new Account
            {
                UserName = username,
                Password = hashedPassword,
                Details = FindOrCreateMember(email,tkid,firstName,lastName)
            };

            repo.Insert(account);

            SaveChanges();

            return account.Id;
        }

        public int CreateCredentials(string username, string password, string email)
        {
            var repo = Repository<Account>();
            if (repo.FindSingle(x => x.UserName == username) != null)
                throw new UsernameInUseException();

            var hashedPassword = PasswordStorage.CreateHash(password);

            var account = new Account
            {
                UserName = username,
                Password = hashedPassword,
                Details = new Member()
                {
                    Email = email
                }
            };

            repo.Insert(account);

            SaveChanges();

            return account.Id;
        }

        public void DeleteCredentials(string username)
        {
            var repo = Repository<Account>();
            var user = repo.FindSingle(x => x.UserName == username);
            if (user == null)
                throw new UserDoesntExistException();
            repo.Delete(user);

            SaveChanges();
        }
    }
}
