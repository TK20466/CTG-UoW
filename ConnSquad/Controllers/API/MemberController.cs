using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CtgModels.Interfaces.Services;

namespace ConnSquad.Controllers.API
{
    [RoutePrefix("api/members")]
    public class MemberController : BaseController
    {
        private IMemberService Service { get; }

        public MemberController()
        {
            Service = Factory.CreateMemberService();
        }

        [HttpGet]
        [Route("GetFeaturedMember")]
        public IHttpActionResult GetFeaturedMember()
        {
            var member = Service.GetFeaturedMember();
            return Ok(member);
        }
    }
}