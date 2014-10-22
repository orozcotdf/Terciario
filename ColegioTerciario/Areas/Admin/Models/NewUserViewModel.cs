using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Areas.Admin.Models
{
    public class NewUserViewModel : ColegioTerciario.Models.RegisterViewModel
    {
        [Required]
        public int USER_PERSONA_ID { get; set; }
        public virtual ColegioTerciario.Models.ApplicationUser USER_PERSONA { get; set; }
    }
}