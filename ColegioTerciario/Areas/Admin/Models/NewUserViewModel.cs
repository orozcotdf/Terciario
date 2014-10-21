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
        public string PERSONA_USER_ID { get; set; }
        public virtual ColegioTerciario.Models.ApplicationUser PERSONA_USER { get; set; }
    }
}