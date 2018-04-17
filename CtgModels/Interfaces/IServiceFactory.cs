using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.Interfaces.Services;

namespace CtgModels.Interfaces
{
    public interface IServiceFactory
    {
        IEventsService CreateEventsService();
        IMemberService CreateMemberService();
        IAuthenticationService CreateAuthenticationService();
    }
}
