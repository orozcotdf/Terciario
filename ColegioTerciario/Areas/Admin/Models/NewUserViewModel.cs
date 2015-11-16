using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Areas.Admin.Models
{
    public class NewUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
        [Required]
        public int USER_PERSONA_ID { get; set; }
        [Required]
        public string USER_PERSONA_ROL { get; set; }
        [Display(Name="DNI")]
        [Required]
        public string UserName { get; set; }
        public virtual ColegioTerciario.Models.User.ApplicationUser USER_PERSONA { get; set; }
    }
}