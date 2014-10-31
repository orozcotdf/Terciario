using ColegioTerciario.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var user = new ApplicationUser();
            user.Email = model.Email;
            user.UserName = model.Email;
            user.USER_PERSONA_ID = model.USER_PERSONA_ID; 
            var result = manager.Create(user, model.Password);
            if (result.Succeeded)
            {
                return user.Id;
            }
            else
            {
                return "";
            }
            
        }


        public ApplicationUser GetUserByPersonaId(int persona_id)
        {
            //context.Users.Where(u => u.USER_PERSONA_ID == persona_id).SingleOrDefault();
            return manager.Users.Where(u => u.USER_PERSONA_ID == persona_id).SingleOrDefault();
        }

        public ApplicationUser GetUser(string user_id)
        {
            return manager.Users.FirstOrDefault(u => u.Id == user_id);
        }
    }
}