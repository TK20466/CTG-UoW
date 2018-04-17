using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnSquad.Controllers.API.Models
{
    public class RegisterModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int LegionId { get; set; }

        [MinLength(8)]
        public string Password { get; set; }

        [MinLength(3)]
        public string UserName { get; set; }

        [MinLength(3)]
        public string ForumHandle { get; set; }
    }
}