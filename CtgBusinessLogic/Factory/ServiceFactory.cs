using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgBusinessLogic.Services;
using CtgDataAccess;
using CtgModels.Interfaces;
using CtgModels.Interfaces.Services;

namespace CtgBusinessLogic.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private IUnitOfWork _workUnit;

        public ServiceFactory()
        {
            _workUnit = new UnitOfWork();
        }

        public IEventsService CreateEventsService()
        {
            return new EventService();
        }

        public IMemberService CreateMemberService()
        {
            return new MemberService();
        }

        public IAuthenticationService CreateAuthenticationService()
        {
            return new AuthenticationService();
        }
    }
}
