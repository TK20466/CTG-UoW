using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Images;
using CtgModels.DataModels.User;
using CtgModels.Enums;

namespace CtgModels.DataModels.Event
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public virtual ICollection<EventParticipation> Participants { get; set; }
        public string Website { get; set; }
        public Image EventImage { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public Contact Requestor { get; set; }
        public bool Display { get; set; }
        public bool WeaponsAllowed { get; set; }
        public EventStatus Status { get; set; }
        public bool LFLApproval { get; set; }
    }
}
