using ColegioTerciario.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.Repositories
{
    public interface IUserRepository
    {
        string CreateUser(NewUserViewModel model);
        ApplicationUser GetUserByPersonaId(int persona_id);
    }
}