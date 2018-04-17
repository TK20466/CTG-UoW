using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.User;

namespace CtgModels.DataModels.Auth
{
    public class ApiToken : BaseEntity
    {
        public virtual Account User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
