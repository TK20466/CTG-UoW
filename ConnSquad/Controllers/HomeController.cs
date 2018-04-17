using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CtgBusinessLogic.Services;

namespace ConnSquad.Controllers
{
    public class HomeController : Controller
    {
        [Route("~/")]
        [Route("home")]
        [Route("events")]
        [Route("events/{*.}")]
        [Route("members")]
        [Route("members/{*.}")]
        [Route("join")]
        [Route("join/{*.}")]
        [Route("login")]
        [Route("login/{*.}")]
        [Route("register")]
        [Route("register/{*.}")]
        [Route("users")]
        [Route("users/{*.}")]
        public ActionResult Index()
       {
            return View();
        }
    }

    public class AccountController : Controller
    {
        [Route("email-verify")]
        [Route("email-verify/{verificationCode}")]
        public ActionResult VerifyEmail(string verificationCode)
        {
            var service = new AuthenticationService();
            try
            {
                if (service.VerifyEmailVerificationCode(new Guid(verificationCode)))
                {
                    return RedirectToAction("Verified");
                }
                else
                    return View("Verify");
            }
            catch (Exception)
            {

                return View("Verify");
            }
        }

        [Route("verification-complete")]
        public ActionResult Verified()
        {
            return View("Verify");
        }
    }
}
