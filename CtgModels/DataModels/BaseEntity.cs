using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.Interfaces.Mappers;

namespace CtgModels.DataModels
{
    public abstract class BaseEntity
    {

        protected BaseEntity() { }

        [Key]
        public int Id { get; set; }
    }
}
