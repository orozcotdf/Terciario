using ColegioTerciario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColegioTerciario.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ColegioTerciarioContext _db;

        public AdminController()
        {
            _db = new ColegioTerciarioContext();
        }

        public ColegioTerciarioContext GetContext()
        {
            return _db;
        }
    }
}