using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ColegioTerciario.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace ColegioTerciario.Models.User
{
   
        public class ApplicationUser : IdentityUser
        {
            public int? USER_PERSONA_ID { get; set; }

            [ForeignKey("USER_PERSONA_ID")]
            public virtual Persona USER_PERSONA { get; set; }
            
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
            {
                // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Agregar reclamaciones de usuario personalizado aquí
                return userIdentity;
            }
        }
    
}