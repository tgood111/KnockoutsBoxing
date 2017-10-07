using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnockoutsBoxing.Models;

namespace KnockoutsBoxing.Controllers
{
    [Authorize(Roles = "Administrator,Moderator,Promoter,Boxer,Fan")]
    public class PollsController : Controller
    {
        private KnockoutsBoxingContext db = new KnockoutsBoxingContext();

        #region standard CRUD stuff of Polls
        // GET: Polls
        public ActionResult Index()
        {
            return View(db.Polls.ToList());
        }

        //ListOfAllPolls
        public ActionResult ListOfAllPolls()
        {
            return View(db.Polls.ToList());
        }

        public ActionResult IndexSimple()
        {
            return View(db.Polls.ToList());
        }

        // GET: Polls/Details/5
        [Authorize(Roles = "Fan")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poll poll = db.Polls.Find(id);
            if (poll == null)
            {
                return HttpNotFound();
            }
            return View(poll);
        }

        [Authorize(Roles = "Fan")]
        // GET: Polls/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Polls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Fan")]
        public ActionResult Create([Bind(Include = "PollID,PollName,PollCreationDate,PollBoxer1,PollBoxer2,PollClosingDate")] Poll poll)
        {
            if (ModelState.IsValid)
            {
                db.Polls.Add(poll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poll);
        }

        // GET: Polls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poll poll = db.Polls.Find(id);
            if (poll == null)
            {
                return HttpNotFound();
            }
            return View(poll);
        }

        // POST: Polls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PollID,PollName,PollCreationDate,PollBoxer1,PollBoxer2,PollClosingDate")] Poll poll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poll);
        }

        // GET: Polls/Delete/5
        [Authorize(Roles = "Administrator,Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poll poll = db.Polls.Find(id);
            if (poll == null)
            {
                return HttpNotFound();
            }
            return View(poll);
        }

        // POST: Polls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Poll poll = db.Polls.Find(id);
            db.Polls.Remove(poll);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region AdditionalMethods


        /*        // GET: Cats/Details/5
        public ActionResult SeeAllToys(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat cat = db.Cats.Find(id);
            ViewBag.CatName = cat.CatName;
            var listoftoys = cat.ToyPurchaseDates;
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(listoftoys.ToList());
        }
         */
        //method to see all the votes.
        // GET: YesOrNoes
        [Authorize(Roles = "Fan")]
        public ActionResult ShowAllVotes(int? id)
        {
            //var yesOrNos = db.YesOrNos.Include(y => y.Poll);
            //return View(yesOrNos.ToList());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Poll poll = db.Polls.Find(id);

            ViewBag.PollName = poll.PollName;

            var listofvotes = poll.PollYesOrNoCollection;
            var countofvotes = listofvotes.Count;
            int countofyesvotes = 0;
            int countofnovotes = 0;

            for(int i=0;i<countofvotes;i++)
            {
                if(listofvotes.ElementAt(i).FansSaidYesOrNO == true)
                {
                    //this means, vote is true
                    countofyesvotes++;
                }
                else
                {
                    countofnovotes++;
                }
            }

            ViewBag.CountYesVotes = countofyesvotes;
            ViewBag.CountNoVotes = countofnovotes;
            ViewBag.TotalVotes = countofnovotes + countofyesvotes;

            return View();
        }

        public ActionResult YesToFight(int? id)
        {
            //var yesOrNos = db.YesOrNos.Include(y => y.Poll);
            //return View(yesOrNos.ToList());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Poll poll = db.Polls.Find(id);

            ViewBag.PollName = poll.PollName;

            YesOrNo yesorno = new YesOrNo();
            yesorno.FansSaidYesOrNO = true;
            yesorno.PollID = poll.PollID;

            db.YesOrNos.Add(yesorno);
            db.SaveChanges();
            return RedirectToAction("Index");

            //return View();
        }

        public ActionResult NoToFight(int? id)
        {
            //var yesOrNos = db.YesOrNos.Include(y => y.Poll);
            //return View(yesOrNos.ToList());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Poll poll = db.Polls.Find(id);

            ViewBag.PollName = poll.PollName;

            YesOrNo yesorno = new YesOrNo();
            yesorno.FansSaidYesOrNO = false;
            yesorno.PollID = poll.PollID;

            db.YesOrNos.Add(yesorno);
            db.SaveChanges();
            return RedirectToAction("Index");

            //return View();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
