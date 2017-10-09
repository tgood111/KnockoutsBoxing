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
    public class PollArticleUsersController : Controller
    {
        private KnockoutsBoxingContext db = new KnockoutsBoxingContext();

        // GET: PollArticleUsers
        public ActionResult Index()
        {
            return View(db.PollArticleUsers.ToList());
        }

        // GET: PollArticleUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PollArticleUsers pollArticleUsers = db.PollArticleUsers.Find(id);
            if (pollArticleUsers == null)
            {
                return HttpNotFound();
            }
            return View(pollArticleUsers);
        }

        // GET: PollArticleUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PollArticleUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PollArticleUsersID")] PollArticleUsers pollArticleUsers)
        {
            if (ModelState.IsValid)
            {
                db.PollArticleUsers.Add(pollArticleUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pollArticleUsers);
        }

        // GET: PollArticleUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PollArticleUsers pollArticleUsers = db.PollArticleUsers.Find(id);
            if (pollArticleUsers == null)
            {
                return HttpNotFound();
            }
            return View(pollArticleUsers);
        }

        // POST: PollArticleUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PollArticleUsersID")] PollArticleUsers pollArticleUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pollArticleUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pollArticleUsers);
        }

        // GET: PollArticleUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PollArticleUsers pollArticleUsers = db.PollArticleUsers.Find(id);
            if (pollArticleUsers == null)
            {
                return HttpNotFound();
            }
            return View(pollArticleUsers);
        }

        // POST: PollArticleUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PollArticleUsers pollArticleUsers = db.PollArticleUsers.Find(id);
            db.PollArticleUsers.Remove(pollArticleUsers);
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
