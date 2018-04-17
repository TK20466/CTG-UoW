using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtgModels.DataModels.User
{
    public class Verification : BaseEntity
    {
        [Required]
        public virtual Account Account { get; set; }

        public Guid Code { get; set; }
    }
}