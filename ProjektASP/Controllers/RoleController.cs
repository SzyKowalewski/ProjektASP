using Microsoft.AspNet.Identity;
using ProjektASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektASP.Controllers
{
    public class RoleController : Controller
    {
        public string Create()
        {
            IdentityManager im = new IdentityManager();

            im.CreateRole("");

            return "OK";
        }


        public string AddToRole()
        {
            IdentityManager im = new IdentityManager();

            im.AddUserToRoleByUsername("test@test.test", "Admin");

            return "OK";
        }
    }
}