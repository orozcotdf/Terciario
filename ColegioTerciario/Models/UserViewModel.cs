using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ColegioTerciario.DAL.Models;

namespace ColegioTerciario.Models
{
    public class RoleViewModel
    {
        public String Name { get; set; }
    }
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string Persona { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public Persona DatosPersonales { get; set; }
    }
}