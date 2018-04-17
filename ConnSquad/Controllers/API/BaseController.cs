using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using ConnSquad.Extensions;
using ConnSquad.Testing;
using CtgBusinessLogic.Factory;
using CtgModels.Enums;
using CtgModels.Interfaces;
using CtgModels.ServiceModels;

namespace ConnSquad.Controllers.API
{
    public abstract class BaseController : ApiController
    {
        protected IServiceFactory Factory { get; }

        protected BaseController()
        {
            Factory = new ServiceFactory();
        }

        public IHttpActionResult CreatedOrUpdated(ServiceResponse response)
        {
            if (response.Result == ServiceResponseType.Updated)
                return new StatusCodeResult(HttpStatusCode.NoContent, this);
            if (response.Result == ServiceResponseType.Created)
            {
                return new StatusCodeResult(HttpStatusCode.Created, this);
            }
            return BadRequest();
        }

        public IHttpActionResult CreatedOrUpdated<T>(ServiceResponse<T> response)
        {
            if (response.Result == ServiceResponseType.Updated)
                return new StatusCodeResult(HttpStatusCode.NoContent, this);
            if (response.Result == ServiceResponseType.Created)
            {
                if (!(response.Data is BaseModel)) return new StatusCodeResult(HttpStatusCode.Created, this);
                var location = GetResolvedLink((response.Data as BaseModel));
                var message = this.Request.CreateResponse(HttpStatusCode.Created, (response.Data as BaseModel).Id);
                message.Headers.Location = new Uri(location, UriKind.Relative);
                return new ResponseMessageResult(message);
            }
            return BadRequest();
        }

        private string GetResolvedLink(BaseModel model)
        {
            return model.GetLink();
        }
    }
}