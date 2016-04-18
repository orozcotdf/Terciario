using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using SendGrid;

namespace ColegioTerciario.Lib
{
    public static class Mailer
    {

        private const string SendGridMailUsername = "azure_fd22ccb1747e880c2d79095cced78667@azure.com";
        private const string SendGridMailPassword = "DgIYom7LOfwZs3x";
        private const string SendGridMailHost = "smtp.sendgrid.net";
        private const string SendGridMailApiKey = "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";

        private static Task sendMailWithSendgrid(string email, string dni, string url)
        {
            // Create the email object first, then add the properties.
            SendGridMessage mensaje = new SendGridMessage();

            mensaje.AddTo(email);
            mensaje.From = new MailAddress("administracion@cent11.edu.ar", "Administracion Cent11");
            mensaje.Subject = "Creando usuario con dni " + dni;
            mensaje.Text = String.Format("<a href='{0}'>Click para activar usuario</a>", url);
            mensaje.Html = String.Format("<a href='{0}'>Click para activar usuario</a>", url);
            mensaje.EnableClickTracking(true);

            // Create an Web transport for sending email.
            var transportWeb = new Web(SendGridMailApiKey);

            // Send the email, which returns an awaitable task.
            return transportWeb.DeliverAsync(mensaje);
        }
        public static void SendMailWithOffice365(string email, string dni, string url)
        {
            try
            {
                using (SmtpClient mailClient = new SmtpClient("smtp.office365.com"))
                {
                    //Your Port gets set to what you found in the "POP, IMAP, and SMTP access" section from Web Outlook  
                    mailClient.Port = 587;
                    //Set EnableSsl to true so you can connect via TLS
                    mailClient.EnableSsl = true;
                    //Your network credentials are going to be the Office 365 email address and the password
                    System.Net.NetworkCredential cred = new System.Net.NetworkCredential("administracion@cent11.edu.ar", "Pa$$word00");
                    mailClient.Credentials = cred;
                    MailMessage message = new MailMessage();
                    //This DOES NOT work  
                    message.From = new MailAddress("administracion@cent11.edu.ar");
                    //This DOES work  
                    message.From = new MailAddress("administracion@cent11.edu.ar", "Administracion Cent11");
                    message.To.Add(email);
                    message.Subject = "Creando usuario con dni " + dni;
                    message.IsBodyHtml = true;
                    message.Body = String.Format("<a href='{0}'>Click para activar usuario</a>", url);
                    mailClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendForgotPasswordMail(string email, string callbackUrl)
        {
            try
            {
                using (SmtpClient mailClient = new SmtpClient("smtp.office365.com", 587))
                {
                    //Your SmtpClient gets set to the SMTP Settings you found in the "POP, IMAP, and SMTP access" section from Web Outlook  
                    //Your Port gets set to what you found in the "POP, IMAP, and SMTP access" section from Web Outlook  
                    mailClient.Port = 587;
                    //Set EnableSsl to true so you can connect via TLS
                    mailClient.EnableSsl = true;
                    //Your network credentials are going to be the Office 365 email address and the password
                    mailClient.Credentials = new System.Net.NetworkCredential("administracion@cent11.edu.ar", "Admin2016");
                    var from = new MailAddress("administracion@cent11.edu.ar", "Administracion Cent11", System.Text.Encoding.UTF8);
                    var to = new MailAddress(email);

                    MailMessage message = new MailMessage(from, to);

                    message.Subject = "Restablecer contraseña";
                    message.IsBodyHtml = true;
                    message.Body = "Para restablecer la contraseña, haga clic <a href=\"" + callbackUrl + "\">aquí</a>";
                    mailClient.Send(message);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static bool SendInscripcion(string email, string url, string html)
        {
            try
            {
                using (SmtpClient mailClient = new SmtpClient("smtp.office365.com", 587))
                {
                    //Your SmtpClient gets set to the SMTP Settings you found in the "POP, IMAP, and SMTP access" section from Web Outlook  
                    //SmtpClient mailClient = new SmtpClient("smtp.office365.com");
                    //Your Port gets set to what you found in the "POP, IMAP, and SMTP access" section from Web Outlook  
                    mailClient.EnableSsl = true;
                    mailClient.UseDefaultCredentials = false;
                    //Set EnableSsl to true so you can connect via TLS
                    //Your network credentials are going to be the Office 365 email address and the password
                    mailClient.Credentials = new System.Net.NetworkCredential("administracion@cent11.edu.ar", "Admin2016");


                    var from = new MailAddress("administracion@cent11.edu.ar", "Administracion Cent11",System.Text.Encoding.UTF8);
                    var to = new MailAddress(email);

                    MailMessage message = new MailMessage(from, to);
                    message.Subject = "Formulario de Inscripcion";
                    message.IsBodyHtml = true;
                    message.Body = html;

                    

                    mailClient.Send(message);
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}