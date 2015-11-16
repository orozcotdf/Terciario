using ColegioTerciario.Areas.Admin.Models;
using ColegioTerciario.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColegioTerciario.Lib;

namespace ColegioTerciario.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ColegioTerciarioContext context;
        private UserStore<ApplicationUser> store;
        private UserManager<ApplicationUser> manager;

        public UserRepository()
        {
            context = new ColegioTerciarioContext();
            store = new UserStore<ApplicationUser>(context);
            manager = new UserManager<ApplicationUser>(store);
        }
        public string CreateUser(NewUserViewModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName,
                USER_PERSONA_ID = model.USER_PERSONA_ID
            };

            var result = manager.Create(user);
            if (result.Succeeded)
            {
                manager.AddToRole(user.Id, model.USER_PERSONA_ROL);
                var usuarioNuevo = context.Users.Single(u => u.Id == user.Id);
                usuarioNuevo.EmailConfirmed = true;

                context.SaveChanges();
                return user.Id;
            }

            return null;

        }


        public ApplicationUser GetUserByPersonaId(int persona_id)
        {
            //context.Users.Where(u => u.USER_PERSONA_ID == persona_id).SingleOrDefault();
            return manager.Users.Where(u => u.USER_PERSONA_ID == persona_id).SingleOrDefault();
        }

        public int GetPersonaIdFromDni(string dni)
        {
            try
            {
                return context.Personas.Single(p => p.PERSONA_DOCUMENTO_NUMERO.Contains(dni)).ID;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ApplicationUser GetUser(string user_id)
        {
            return manager.Users.FirstOrDefault(u => u.Id == user_id);
        }
    }
}