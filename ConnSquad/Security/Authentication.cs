using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ConnSquad.Testing;
using CtgModels.Enums;
using CtgModels.Interfaces;
using CtgModels.Interfaces.Services;

namespace ConnSquad.Security
{
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        public AuthenticationFilter() : base()
        {
            IServiceFactory factory = new MockServiceFactory();
            AuthenticationService = factory.CreateAuthenticationService();
        }
        public IAuthenticationService AuthenticationService { get; set; }

        /// <summary>
        /// read requested header and validated
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext)) return;
            var identity = FetchFromHeader(actionContext);

            if (identity != Guid.Empty)
            {
                var user = AuthenticationService.ValidateAPIToken(identity);
                if (user != null)
                {
                    IPrincipal principal = new CTGPrincipal(new CTGIdentity(user));
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                    actionContext.RequestContext.Principal = principal;
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        /// <summary>
        /// retrive header detail from the request 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private Guid FetchFromHeader(HttpActionContext actionContext)
        {
            Guid requestToken = Guid.Empty;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Bearer")
                requestToken = new Guid(authRequest.Parameter);

            return requestToken;
        }
    }

    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeRoleAttribute()
        {
            CommandTeam = true;
        }

        public AuthorizeRoleAttribute(MemberRankEnum value)
        {
            Rank = value;
        }

        public bool CommandTeam { get; set; }
        public MemberRankEnum Rank { get; set; }

        public CTGPrincipal User => (CTGPrincipal) HttpContext.Current.User;

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authorized = base.IsAuthorized(actionContext);
            if (!authorized)
                return false;

            if (CommandTeam)
            {
                return User.IsCommandTeam();
            }

            return User.IsInRole(Rank);
        }
    }
}