﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace veb.Controllers
{
    public class ExempelProfilController : Controller
    {
        // GET: ExempelProfil

        public ActionResult Exprofil()
        {  
       
            ViewBag.Message = "exempelprofil sida";
            return View();
        }
    }
}