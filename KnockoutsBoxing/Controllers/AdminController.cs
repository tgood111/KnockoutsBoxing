using KnockoutsBoxing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnockoutsBoxing.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            //var listofallusers = db.Users.OrderBy(x => x.Email);
            var listofallusers = db.Users.OrderBy(x => x.Email);
            int count = listofallusers.Count();

            return View(listofallusers);
        }
    }
}