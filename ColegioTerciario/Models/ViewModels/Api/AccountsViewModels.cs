using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels.Api
{
    public class BatchCreateUsersVM
    {
        public string Email { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public class BatchCreateUsersResponseVM
    {
        public string Dni { get; set; }
        public bool MailEnviado { get; set; }
    }
}