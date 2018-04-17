using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.Enums;
using CtgModels.ServiceModels;

namespace CtgModels.Interfaces.Services
{
    public interface IEventsService
    {
        IEnumerable<Event> FutureEvents(int count);
        ServiceResponse<Event> CreateEvent(Event eventModel);
        ServiceResponse DeleteEvent(int eventId);
        ServiceResponse AddParticipant(int eventId, int memberId, int costumeId);
        ServiceResponse AddWrangler(int eventId, int memberId);
        ServiceResponse RemoveParticipant(int eventId, int memberId);
        ServiceResponse ChangeParticipantCostume(int eventId, int memberId, int costumeId);
        IEnumerable<Event> GetEvents();
    }
}
