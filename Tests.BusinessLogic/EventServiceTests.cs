using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgBusinessLogic.Services;
using CtgModels.Enums;
using CtgModels.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.BusinessLogic
{
    [TestClass]
    public class EventServiceTests
    {
        private Event GetTestEvent(string title)
        {
            return new Event()
            {
                Address = "39 Victoria Street",
                Date = DateTime.UtcNow,
                Description = "Some Event",
                Display = true,
                EndDate = DateTime.UtcNow.AddDays(5),
                LFLApproval = true,
                Requestor = new Contact()
                {
                    Email = "sarahbourt@gmail.com",
                    FirstName = "Sarah",
                    LastName = "Bourt"
                },
                State = "Connecticut",
                Status = EventStatus.Confirmed,
                Title = title,
                Town = "Windsor",
                WeaponsAllowed = true,
                ZipCode = "06095",
                Wesbite = "ctg.sarah-bailey.com"
            };
        }

        [TestMethod]
        public void CreateEvent()
        {
            var service = new EventService();
            var testEvent = GetTestEvent("Create Test");

            var response = service.CreateEvent(testEvent);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Id > 0);
        }

        [TestMethod]
        public void DropEvent()
        {
            // setup
            var service = new EventService();
            var testEvent = GetTestEvent("To Drop");
            var eventid = service.CreateEvent(testEvent).Data.Id;

            var response = service.DeleteEvent(eventid);

            Assert.AreEqual(response.Result, ServiceResponseType.Deleted);
        }
    }
}
