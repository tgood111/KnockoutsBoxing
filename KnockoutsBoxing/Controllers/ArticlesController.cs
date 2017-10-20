using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnockoutsBoxing.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.IO;

namespace KnockoutsBoxing.Controllers
{
    [Authorize(Roles = "Administrator,Moderator,Promoter,Boxer,Fan")]
    public class ArticlesController : Controller
    {
        private KnockoutsBoxingContext db = new KnockoutsBoxingContext();


        #region standard CRUD stuff
        // GET: Articles
        public ActionResult Index()
        {
            var user = User.Identity.Name;

            return View(db.Articles.ToList());
        }

        //ListOfAllArticles
        public ActionResult ListOfAllArticles()
        {
            return View(db.Articles.ToList());
        }

        //ListOfAllArticlesUser
        public ActionResult ListOfAllArticlesUser()
        {

            var allarticles = db.Articles;
            string currentuser = User.Identity.Name;

            var currentuserarticles = from article in allarticles where article.ArticleCreatedBy == currentuser select article;
         
            return View(currentuserarticles.ToList());
        }

        public ActionResult IndexSimple()
        {
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Comments/Details/5
        public ActionResult CommentDetails(int? id)
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

        // GET: Articles/Details/5
        //public async Task<ActionResult> ChangeToBoxer(string UserName)
        public async Task<ActionResult> FlagComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            comment.FlagComment = true;
            if (comment == null)
            {
                return HttpNotFound();
            }
            await db.SaveChangesAsync();
            return View(comment);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,ArticleTitle,ArticleCreationDate,ArticleAuthor,ArticleContent,imagefile")] Article article)
        {
            var user = User.Identity.Name;
            article.ArticleCreatedBy = user;
            article.ArticleCreationDate = DateTime.Now;
            article.ArticleAuthor = user;
            //location
            //var filename1 = Path.GetFileName(article.imagefile.FileName);
            var filename1 = Path.GetFileNameWithoutExtension(article.imagefile.FileName);
            filename1 = filename1 + DateTime.Now.Ticks;
            var extensionoffile = Path.GetExtension(article.imagefile.FileName);
            filename1 = filename1 + extensionoffile;
            var SaveLocation = Path.Combine(Server.MapPath("~/Content/images"), filename1);
            article.ImageFileName = filename1;
            article.imagefile.SaveAs(SaveLocation);
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,ArticleTitle,ArticleCreationDate,ArticleAuthor,ArticleContent")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Administrator,Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region additional methods

        //one method to add comment

        //one method to show all comments.

        public ActionResult ShowAllComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = db.Articles.Find(id);

            ViewBag.ArticleName = article.ArticleTitle;

            var listofcomments = article.Comments;

            return View(listofcomments.ToList());
        }



        //GET method
        public ActionResult AddComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = db.Articles.Find(id);

            ViewBag.ArticleName = article.ArticleTitle;

            Comment comment = new Comment();
            comment.ArticleID = article.ArticleID;
            comment.CommentAuthor = "Logged In User";

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment([Bind(Include = "CommentID,CommentContent,CommentAuthor,ArticleID")] Comment comment)
        {
            var user = User.Identity.Name;
            comment.CommentCreatedBy = user;

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(comment);
        }


        /*
   [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCatToy([Bind(Include = "ToyPurchaseDateID,DateOfPurchase,ToyName,CatID")] ToyPurchaseDate toyPurchaseDate)
        {
            //[Bind(Include = "ToyPurchaseDateID,DateOfPurchase,ToyName,CatID")] ToyPurchaseDate toyPurchaseDate

            if (ModelState.IsValid)
            {
                db.ToyPurchaseDates.Add(toyPurchaseDate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toyPurchaseDate);
        }



                     */

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
