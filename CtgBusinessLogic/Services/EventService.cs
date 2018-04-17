using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Costume;
using CtgModels.DataModels.User;
using CtgModels.Enums;
using CtgModels.Exceptions.Data;
using CtgModels.Extensions;
using CtgModels.Interfaces.Services;
using CtgModels.Mappers;
using CtgModels.ServiceModels;
using ContactEntity = CtgModels.DataModels.Event.Contact;
using EventEntity = CtgModels.DataModels.Event.Event;
using MemberEntity = CtgModels.DataModels.User.Member;
using ImageEntity = CtgModels.DataModels.Images.Image;
using EventParticipation = CtgModels.DataModels.User.EventParticipation;

namespace CtgBusinessLogic.Services
{
    public class EventService : Service<EventEntity, Event>, IEventsService
    {
        /// <summary>
        /// Gets the future events for public display
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Event> FutureEvents(int count)
        {
            return Repository<EventEntity>()
                .FindBy(x => x.Display && x.Status == EventStatus.Confirmed)
                .OrderByDescending(x => x.Date)
                .Take(count)
                .MakeConcrete()
                .AsModel<Event, EventEntity>("EventImage");
        }

        public ServiceResponse<Event> CreateEvent(Event eventModel)
        {
            Event completed;

            //check for existence
            if (eventModel.Id == default(int))
            {
                completed = _create(eventModel);
                return new ServiceResponse<Event>(completed, ServiceResponseType.Created);
            }

            completed = _update(eventModel);
            return new ServiceResponse<Event>(completed, ServiceResponseType.Updated);
        }

        private Event _create(Event eventModel)
        {
            var entity = new EventEntity()
            {
                Date = eventModel.Date,
                EndDate = eventModel.EndDate,
                Display = eventModel.Display,
                LFLApproval = eventModel.LFLApproval,
                State = eventModel.State,
                Town = eventModel.Town,
                Website = eventModel.Wesbite,
                ZipCode = eventModel.ZipCode,
                WeaponsAllowed = eventModel.WeaponsAllowed,
                Status = eventModel.Status,
                Address = eventModel.Address,
                Name = eventModel.Title,
                Description = eventModel.Description,
                Requestor = this.GetOrCreateRequestor(eventModel.Requestor)
            };
            Repository<EventEntity>().Insert(entity);
            SaveChanges();
            eventModel.Id = entity.Id;
            return eventModel;
        }

        private Event _update(Event model)
        {
            var entity = Repository<EventEntity>().FindSingle(x => x.Id == model.Id);
            entity.Name = model.Title;
            entity.Date = model.Date;
            entity.EndDate = model.EndDate;
            entity.LFLApproval = model.LFLApproval;
            entity.Display = model.Display;
            entity.Status = model.Status;
            entity.State = model.State;
            entity.ZipCode = model.ZipCode;
            entity.WeaponsAllowed = model.WeaponsAllowed;
            entity.Website = model.Wesbite;
            entity.Description = model.Description;
            entity.Town = model.Town;
            entity.Address = model.Address;
            SaveChanges();
            return model;
        }

        public ServiceResponse SetEventImage(int eventId, Image img)
        {
            var eventity = this.GetEvent(eventId);
            eventity.EventImage = new ImageEntity
            {
                Format = img.Format,
                ImageType = img.ImageType,
                Url = img.Url
            };
            SaveChanges();

            return new ServiceResponse(ServiceResponseType.Created);
        }

        public ServiceResponse DeleteEvent(int eventId)
        {
            var entity = Repository<EventEntity>().FindSingle(x => x.Id == eventId);
            if (entity == null)
            {
                throw new RowNotInTableException("Event Id Not Found");
            }

            Repository<EventEntity>().Delete(entity);
            SaveChanges();
            return new ServiceResponse(ServiceResponseType.Deleted);
        }

        public ServiceResponse AddParticipant(int eventId, int memberId, int costumeId)
        {
            var eventity = this.GetEvent(eventId);
            var member = this.GetMember(memberId);
            var costume = member.Costumes.FirstOrDefault(x => x.Id == costumeId);
            if (costume == null)
            {
                throw new DbException(DbExceptionReason.CostumeNotExist, "Member does not have costume");
            }

            var participationRecord = new EventParticipation
            {
                CostumeId = costume.Id,
                MemberId = member.Id,
                EventId = eventity.Id
            };

            Repository<EventParticipation>().Insert(participationRecord);
            SaveChanges();

            return new ServiceResponse(ServiceResponseType.Created);
        }

        public ServiceResponse AddWrangler(int eventId, int memberId)
        {
            var eventity = this.GetEvent(eventId);
            var member = this.GetMember(memberId);
            var participationRecord = new EventParticipation
            {
                MemberId = member.Id,
                EventId = eventity.Id
            };

            Repository<EventParticipation>().Insert(participationRecord);
            SaveChanges();

            return new ServiceResponse(ServiceResponseType.Created);
        }

        public ServiceResponse RemoveParticipant(int eventId, int memberId)
        {
            var eventity = this.GetEvent(eventId);
            var participation = eventity.Participants.Where(x => x.MemberId == memberId).ToList();

            if (!participation.Any())
            {
                return new ServiceResponse(ServiceResponseType.NoChange);
            }

            Repository<EventParticipation>().Delete(participation);
            SaveChanges();
            return new ServiceResponse(ServiceResponseType.Deleted);
        }

        public ServiceResponse ChangeParticipantCostume(int eventId, int memberId, int costumeId)
        {
            var eventity = this.GetEvent(eventId);
            var member = eventity.Participants.FirstOrDefault(x => x.MemberId == memberId);
            if (member == null)
            {
                throw new DbException(DbExceptionReason.MemberNotExist, "Member has not signed up for this event");
            }
            member.Costume = null;
            member.CostumeId = costumeId;
            SaveChanges();

            return new ServiceResponse(ServiceResponseType.Updated);
        }

        public IEnumerable<Event> GetEvents()
        {
            return Repository<EventEntity>()
                .Select()
                .OrderByDescending(x => x.Date)
                .MakeConcrete()
                .Select(this.ServiceModel);
        }

        private ContactEntity GetOrCreateRequestor(Contact contact)
        {
            var find = Repository<ContactEntity>().FindSingle(x => x.Id == contact.Id || (x.Email == contact.Email && x.FirstName == contact.FirstName && x.LastName == contact.LastName));
            if (find != null) return find;


            return new ContactEntity
            {
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            };
        }
    }
}
