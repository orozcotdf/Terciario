using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels
{
    
    public class UserViewModel
    {
        public String Name { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}