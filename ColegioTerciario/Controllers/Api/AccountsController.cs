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
using ColegioTerciario.Models.User;
using ColegioTerciario.Models.ViewModels.Api;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SendGrid;

namespace ColegioTerciario.Controllers.Api
{
    public class AccountsController : ApiController
    {

        private const string MailUsername = "azure_fd22ccb1747e880c2d79095cced78667@azure.com";
        private const string MailPassword = "DgIYom7LOfwZs3x";
        private const string MailHost = "smtp.sendgrid.net";
        private const string MailApiKey = "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";
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
                        continue;
                    }

                    var usuario = new ApplicationUser { UserName = user.Dni, Email = user.Email };

                    // Crea el usuario
                    var result = UserManager.Create(usuario);
                    if (result.Succeeded)
                    {
                        // Lo agrega al rol Docente
                        UserManager.AddToRole(usuario.Id, "Docente");
                        string code = UserManager.GeneratePasswordResetToken(usuario.Id);
                        var url = Url.Route("Default", new { controller = "Account", action = "ResetPassword", code = code });
                        var urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                        var persona = _db.Personas.SingleOrDefault(p => p.PERSONA_DOCUMENTO_NUMERO.Trim() == user.Dni.Trim());
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
                        // Create the email object first, then add the properties.
                        SendGridMessage mensaje = new SendGridMessage();

                        mensaje.AddTo(user.Email);
                        mensaje.From = new MailAddress("administracion@cent11.edu.ar", "Administracion Cent11");
                        mensaje.Subject = "Creando usuario con dni " + user.Dni;
                        mensaje.Text = String.Format("<a href='{0}'>Click para activar usuario</a>", urlBase + url);
                        mensaje.Html = String.Format("<a href='{0}'>Click para activar usuario</a>", urlBase + url);
                        mensaje.EnableClickTracking(true);

                        // Create an Web transport for sending email.
                        var transportWeb = new Web(MailApiKey);

                        // Send the email, which returns an awaitable task.
                        await transportWeb.DeliverAsync(mensaje);

                        var datos = new BatchCreateUsersResponseVM
                        {
                            Dni = user.Dni,
                            MailEnviado = true
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
