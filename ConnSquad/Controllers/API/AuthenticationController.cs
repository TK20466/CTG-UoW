namespace ConnSquad.Controllers.API
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Results;
    using ConnSquad.Controllers.API.Models;
    using ConnSquad.Security;
    using CtgBusinessLogic.Utilities;
    using CtgModels.Enums;
    using CtgModels.Exceptions.Tokens;
    using CtgModels.Interfaces.Services;

    [AuthenticationFilter]
    [RoutePrefix("api/auth")]
    public class AuthenticationController : BaseController
    {

        public AuthenticationController()
        {
            Service = Factory.CreateAuthenticationService();
        }

        public IAuthenticationService Service { get; set; }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IHttpActionResult CreateToken(LoginModel model)
        {
            try
            {
                var id = Service.ValidateCredentials(model.Username, model.Password);
                var token = Service.GetAPIToken(id);
                return Ok(token);
            }
            catch (Exception e)
            {

                if (e is UserNotVerifiedException)
                {
                    return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "Email verification not completed"
                    });
                }

                if ( e is UserDoesntExistException || e is UserPasswordWrongException)
                    return Unauthorized();
                return InternalServerError(e);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterUser")]
        public IHttpActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);   
            }
            var account = Service.CreateCredentials(model.UserName, model.Password, model.Email, model.LegionId,
                model.FirstName, model.LastName);
            var verificationCode = Service.CreateVerificationCode(account);
            MailUtilities.SendEmail(model.Email, verificationCode, GetVerificationPath());
            return Ok("Verification Email Sent");
        }

        private string GetVerificationPath()
        {
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            return baseUrl + "/email-verify/";
        }

        [HttpGet]
        [AuthorizeRole]
        [Route("TestAny")]
        public IHttpActionResult TestAny()
        {
            return Ok("Okay");
        }

        [HttpGet]
        [AuthorizeRole(MemberRankEnum.WebMaster)]
        [Route("TestWebMaster")]
        public IHttpActionResult TestAnyWebMaster( )
        {
            return Ok("Okay");
        }

        [HttpGet]
        [AuthorizeRole(MemberRankEnum.CommandingOfficer)]
        [Route("TestCO")]
        public IHttpActionResult TestAnyCO( )
        {
            return Ok("Okay");
        }
    }
}