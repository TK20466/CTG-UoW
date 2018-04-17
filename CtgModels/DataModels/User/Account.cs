using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Auth;

namespace CtgModels.DataModels.User
{
    public class Account : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Verified { get; set; }
        public virtual Member Details { get; set; }

        public virtual Verification Verification { get; set; }
    }
}
