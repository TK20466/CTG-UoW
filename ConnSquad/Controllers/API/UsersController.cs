using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConnSquad.Security;

namespace ConnSquad.Controllers.API
{
    [AuthenticationFilter]
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {
        public UsersController()
        {
            //Service = Factory.CreateAccountService();
        }
        //public IAccountService Service { get; set; }
    }
}
