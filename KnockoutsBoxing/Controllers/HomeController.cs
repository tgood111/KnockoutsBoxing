using KnockoutsBoxing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnockoutsBoxing.Controllers
{
    public class HomeController : Controller
    {

        private KnockoutsBoxingContext db = new KnockoutsBoxingContext();
        //I dont want to restrict Home i.e. Index
        public ActionResult Index()
        {
            //get a list of all Polls
            var listofallpolls = db.Polls.ToList();
            var listofallarticles = db.Articles.ToList();
            var listofallcomments = db.Comments.ToList();

            PollArticleUsers pollarticleusers = new PollArticleUsers();
            pollarticleusers.Polls = listofallpolls;
            pollarticleusers.Articles = listofallarticles;
            pollarticleusers.Comments = listofallcomments;
            return View(pollarticleusers);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}