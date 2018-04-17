using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConnSquad.Controllers.API.Models;
using ConnSquad.Security;
using CtgModels.Interfaces.Services;
using CtgModels.ServiceModels;
using Moq;

namespace ConnSquad.Controllers.API
{
    [AuthenticationFilter]
    [RoutePrefix("api/events")]
    public class EventsController : BaseController
    {
        private IEventsService Service { get; }

        public EventsController()
        {
            Service = Factory.CreateEventsService();
        }

        [HttpGet]
        [Route("GetFutureEvents/{count}")]
        public IHttpActionResult FutureEvents(int count)
        {
            return Ok(Service.FutureEvents(count));
        }

        [HttpGet]
        [Route("fetch")]
        public IHttpActionResult GetEvents()
        {
            var events = Service.GetEvents();
            return Ok(events);
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateEvent(Event model)
        {
            var response = Service.CreateEvent(model);
            return CreatedOrUpdated(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult CreateEvent(int id, Event model)
        {
            var response = Service.CreateEvent(model);
            return CreatedOrUpdated(response);
        }
        
            
    }
}
