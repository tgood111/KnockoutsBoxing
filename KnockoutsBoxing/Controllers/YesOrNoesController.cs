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
    public class YesOrNoesController : Controller
    {
        private KnockoutsBoxingContext db = new KnockoutsBoxingContext();

        // GET: YesOrNoes
        public ActionResult Index()
        {
            var yesOrNos = db.YesOrNos.Include(y => y.Poll);
            return View(yesOrNos.ToList());
        }

        // GET: YesOrNoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YesOrNo yesOrNo = db.YesOrNos.Find(id);
            if (yesOrNo == null)
            {
                return HttpNotFound();
            }
            return View(yesOrNo);
        }

        // GET: YesOrNoes/Create
        public ActionResult Create()
        {
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "PollName");
            return View();
        }

        // POST: YesOrNoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YesOrNoID,FansSaidYesOrNO,PollID")] YesOrNo yesOrNo)
        {
            if (ModelState.IsValid)
            {
                db.YesOrNos.Add(yesOrNo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PollID = new SelectList(db.Polls, "PollID", "PollName", yesOrNo.PollID);
            return View(yesOrNo);
        }

        // GET: YesOrNoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YesOrNo yesOrNo = db.YesOrNos.Find(id);
            if (yesOrNo == null)
            {
                return HttpNotFound();
            }
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "PollName", yesOrNo.PollID);
            return View(yesOrNo);
        }

        // POST: YesOrNoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YesOrNoID,FansSaidYesOrNO,PollID")] YesOrNo yesOrNo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yesOrNo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "PollName", yesOrNo.PollID);
            return View(yesOrNo);
        }

        // GET: YesOrNoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YesOrNo yesOrNo = db.YesOrNos.Find(id);
            if (yesOrNo == null)
            {
                return HttpNotFound();
            }
            return View(yesOrNo);
        }

        // POST: YesOrNoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YesOrNo yesOrNo = db.YesOrNos.Find(id);
            db.YesOrNos.Remove(yesOrNo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
