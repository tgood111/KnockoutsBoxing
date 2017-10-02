using KnockoutsBoxing.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //public async Task<ActionResult> UserDetails(string UserName)
        public async Task<ActionResult> UserDetails(string UserName)
        {
            /*
             *             //This will collect the user with the sent user name
            var tempuser = UserManager.Users.First(x => x.UserName.Equals(UserName));
            //this will collect the user role 
            var temp = await UserManager.GetRolesAsync(tempuser.Id);
            ViewBag.ObtainedUserRole = temp[0];
            return View(tempuser);
             */

            //first I would like to get the User whom's role I want to see
            var CurrentUser = UserManager.Users.First(u => u.UserName.Equals(UserName));


            //I want to collect the role he has. 
            var CurrentUserRole = await UserManager.GetRolesAsync(CurrentUser.Id);

            if(CurrentUserRole.Count>0)
            {
                ViewBag.UserRole = CurrentUserRole[0];
            }

            return View();


        }

        //public async Task<ActionResult> UserUpgradeToCanSee(string UserName)
        //ChangeToAdmin

        public async Task<ActionResult> ChangeToAdmin(string UserName)
        {
            //first I would like to get the User whom's role I want to see
            var CurrentUser = UserManager.Users.First(u => u.UserName.Equals(UserName));

            //I want to collect the role he has. 
            var CurrentUserRole = await UserManager.GetRolesAsync(CurrentUser.Id);

            if (CurrentUserRole.Count > 0)
            {
                ViewBag.OldUserRole = CurrentUserRole[0];
                /*
                 *             //remove existing role
                    await UserManager.RemoveFromRoleAsync(tempuser.Id, temp[0]);
                    var temp1 = await UserManager.GetRolesAsync(tempuser.Id);
                 */
                await UserManager.RemoveFromRoleAsync(CurrentUser.Id, CurrentUserRole[0]);
            }
            else
            {
                ViewBag.OldUserRole = "No Current Role Assigned";
            }
            //var result_of_role_change = await UserManager.AddToRoleAsync(tempuser.Id, "canSee");
            var ResultRoleChange = await UserManager.AddToRoleAsync(CurrentUser.Id, "Administrator");

            var NewRole = await UserManager.GetRolesAsync(CurrentUser.Id);
            ViewBag.NewUserRole = NewRole[0];

            await db.SaveChangesAsync();

            return View();
        }

        //This is that incredibly important UserManager that is used everywhere
        //I copied this from the other controllers who are also using this method extensively
        //to work on users that are extracted
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //this is an object that allows you to use the UserManager
        //UserManage is like a proxy for doing db operations on the user object
        //its pretty userful
        private ApplicationUserManager _userManager;
    }


}