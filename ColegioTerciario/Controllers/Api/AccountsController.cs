using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using ColegioTerciario.Lib;
using ColegioTerciario.Models.User;
using ColegioTerciario.Models.ViewModels.Api;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SendGrid;

namespace ColegioTerciario.Controllers.Api
{
    public class AccountsController : ApiController
    {
        private ApplicationUserManager _userManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    var manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    return manager;
                }
                else
                {
                    return _userManager;
                }

            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: api/Accounts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Accounts/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Accounts
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Accounts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Accounts/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public IHttpActionResult ResetPassword(string userId)
        {
            ApplicationUser user = UserManager.FindById(userId);
            string code = UserManager.GeneratePasswordResetToken(user.Id);
            var url = Url.Route("Default", new { controller = "Account", action = "ResetPassword", code = code });
            var urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);

            Mailer.SendForgotPasswordMail(user.Email, urlBase + url);
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> BatchCreateUsers(List<BatchCreateUsersVM> users)
        {
            
            var _db = new ColegioTerciario.Models.ColegioTerciarioContext();
            var respuesta = new List<BatchCreateUsersResponseVM>();

            foreach (BatchCreateUsersVM user in users)
            {
                try
                {
                    // Ignora el proceso si el Usuario ya existe
                    if (UserManager.FindByName(user.Dni) != null)
                    {
                        throw new Exception("El usuario ya existe");

                    }

                    var usuario = new ApplicationUser { UserName = user.Dni, Email = user.Email };

                    // Crea el usuario
                    var result = UserManager.Create(usuario);
                    if (result.Succeeded)
                    {

                        // Lo agrega al rol Docente
                        UserManager.AddToRole(usuario.Id, "Docente");
                        string code = UserManager.GeneratePasswordResetToken(usuario.Id);

                        var usuarioNuevo = _db.Users.Single(u => u.Id == usuario.Id);
                        usuarioNuevo.EmailConfirmed = true;

                        _db.SaveChanges();

                        var url = Url.Route("Default",
                            new {controller = "Account", action = "ResetPassword", code = code.Base64ForUrlEncode()});
                        var urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                        var persona = _db.Personas.SingleOrDefault(
                            p =>
                                p.PERSONA_DOCUMENTO_NUMERO.Trim().Contains(user.Dni.Trim()) && p.PERSONA_ES_DOCENTE == true
                            );
                        // Si existe una persona con el mismo ID le setea algunos valores
                        if (persona != null)
                        {
                            var savedUser = _db.Users.Find(usuario.Id);
                            savedUser.USER_PERSONA_ID = persona.ID;
                            persona.PERSONA_EMAIL = user.Email.ToUpper();
                            persona.PERSONA_ES_DOCENTE = true;
                            persona.PERSONA_NOMBRE = user.Nombre.ToUpper();
                            persona.PERSONA_APELLIDO = user.Apellido.ToUpper();
                            _db.SaveChanges();
                        }

                        //await sendMailWithSendgrid(user.Email, user.Dni, urlBase + url);
                        Mailer.SendMailWithOffice365(user.Email, user.Dni, urlBase + url);

                        var datos = new BatchCreateUsersResponseVM
                        {
                            Dni = user.Dni,
                            MailEnviado = true,
                            Mensaje = "Usuario creado"
                        };
                        respuesta.Add(datos);
                    }
                    else
                    {
                        var usuarioExistente = UserManager.FindByEmail(user.Email);
                        string code = UserManager.GeneratePasswordResetToken(usuarioExistente.Id);
                        var url = Url.Route("Default",
                            new { controller = "Account", action = "ResetPassword", code = code });
                        var urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                        Mailer.SendMailWithOffice365(user.Email, user.Dni, urlBase + url);
                        var datos = new BatchCreateUsersResponseVM
                        {
                            Dni = user.Dni,
                            MailEnviado = true,
                            Mensaje = "El usuario existia, se envio mail para reinicio de contraseña"
                        };
                        respuesta.Add(datos);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }

            

            return Ok(respuesta);
        }

        
    }
}
