using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.Enums;

namespace CtgModels.ServiceModels
{
    public class Event : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<EventParticipation> Participants { get; set; }
        public Contact Requestor { get; set; }
        public Image FeaturedImage { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Wesbite { get; set; }
        public bool LFLApproval { get; set; }
        public EventStatus Status { get; set; }
        public bool WeaponsAllowed { get; set; }
        public bool Display { get; set; }

        //Build properties

        public string Location => $"{Address}, {Town} {State}, {ZipCode}";
    }
}
