using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnockoutsBoxing.Models;
using System.Threading.Tasks;

namespace KnockoutsBoxing.Controllers
{
    [Authorize(Roles = "Administrator,Moderator,Promoter,Boxer,Fan")]
    public class CommentsController : Controller
    {
        private KnockoutsBoxingContext db = new KnockoutsBoxingContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Article);
            return View(comments.ToList());
        }

        //ListOfAllComments
        public ActionResult ListOfAllComments()
        {
            var comments = db.Comments.Include(c => c.Article);
            return View(comments.ToList());
        }

        public async Task<ActionResult> UnFlagComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            comment.FlagComment = false;
            if (comment == null)
            {
                return HttpNotFound();
            }
            await db.SaveChangesAsync();
            return View(comment);
        }

        //ListOfAllComments
        public ActionResult ListOfAllFlaggedComments()
        {
            var comments = db.Comments.Include(c => c.Article);
            return View(comments.ToList());
        }


        //ListOfAllCommentsUser
        public ActionResult ListOfAllCommentsUser()
        {
            var allcomments = db.Comments;

            string currentuser = User.Identity.Name;

            var currentusercomments = from comment in allcomments where comment.CommentCreatedBy == currentuser select comment;
            return View(currentusercomments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleTitle");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,CommentContent,CommentAuthor,ArticleID")] Comment comment)
        {
            var user = User.Identity.Name;
            comment.CommentCreatedBy = user;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleTitle", comment.ArticleID);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleTitle", comment.ArticleID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,CommentContent,CommentAuthor,ArticleID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleTitle", comment.ArticleID);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
