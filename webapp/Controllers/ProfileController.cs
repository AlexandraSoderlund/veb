using Datalager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public ActionResult SaveProfile(EditProfileViewModel model)
        {
            using (var db = new DejtDbContext())
            {

                return View();
            }
        }
    }
}