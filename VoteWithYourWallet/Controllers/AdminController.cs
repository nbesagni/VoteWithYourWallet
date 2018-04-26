using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoteWithYourWallet.Models;

namespace VoteWithYourWallet.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            
                return View();
        }

 
    }
}