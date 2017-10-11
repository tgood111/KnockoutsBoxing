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
    [Authorize(Roles = "Administrator")]
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

        [Authorize(Roles = "Moderator,Administrator")]
        public ActionResult ModeratorIndex()
        {
            //var listofallusers = db.Users.OrderBy(x => x.Email);
            var listofallusers = db.Users.OrderBy(x => x.Email);
            int count = listofallusers.Count();

            return View(listofallusers);
        }

        [Authorize(Roles = "Promoter,Administrator")]
        public ActionResult PromotorIndex()
        {
            //var listofallusers = db.Users.OrderBy(x => x.Email);
            var listofallusers = db.Users.OrderBy(x => x.Email);
            int count = listofallusers.Count();

            return View(listofallusers);
        }

        [Authorize(Roles = "Fighter,Administrator")]
        public ActionResult FighterIndex()
        {
            //var listofallusers = db.Users.OrderBy(x => x.Email);
            var listofallusers = db.Users.OrderBy(x => x.Email);
            int count = listofallusers.Count();

            return View(listofallusers);
        }

        public ActionResult ListOfAllUsers()
        {
            //var listofallusers = db.Users.OrderBy(x => x.Email);
            var listofallusers = db.Users.OrderBy(x => x.Email);
            int count = listofallusers.Count();

            return View(listofallusers);
        }

        public ActionResult ListOfAllUsersModerator()
        {
            //var listofallusers = db.Users.OrderBy(x => x.Email);
            var listofallusers = db.Users.OrderBy(x => x.Email);
            int count = listofallusers.Count();

            return View(listofallusers);
        }

        //@Html.ActionLink("Ban User", "BanUser", new { UserName = item.UserName}) |
        public async Task<ActionResult> BanUser(string UserName)
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

            //Important Note - if phone number is 9999999999 user is blocked
            //this is used in Login method of AccountController
            //CurrentUser.PhoneNumber = "9999999999";
            try
            {
                var result = await UserManager.SetPhoneNumberAsync(CurrentUser.Id, "9999999999");
            }
            catch
            {
                string temp = "something went wrong";
            }
            
            return View();


        }

        //   @Html.ActionLink("Remove Ban", "RemoveBanUser", new { UserName = item.UserName})

        public async Task<ActionResult> RemoveBanUser(string UserName)
        {
            //first I would like to get the User whom's role I want to see
            var CurrentUser = UserManager.Users.First(u => u.UserName.Equals(UserName));
            
            //Important Note - if phone number is 9999999999 user is blocked
            //changing it to something else makes is nonblocked
            //this is used in Login method of AccountController
            await UserManager.SetPhoneNumberAsync(CurrentUser.UserName, "");
            return View();

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

        #region Role Change 
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
        }//end of ChangeToAdmin

        public async Task<ActionResult> ChangeToModerator(string UserName)
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
            var ResultRoleChange = await UserManager.AddToRoleAsync(CurrentUser.Id, "Moderator");

            var NewRole = await UserManager.GetRolesAsync(CurrentUser.Id);
            ViewBag.NewUserRole = NewRole[0];

            await db.SaveChangesAsync();

            return View();
        }//end of ChangeToModerator

        public async Task<ActionResult> ChangeToPromotor(string UserName)
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
            var ResultRoleChange = await UserManager.AddToRoleAsync(CurrentUser.Id, "Promoter");

            var NewRole = await UserManager.GetRolesAsync(CurrentUser.Id);
            ViewBag.NewUserRole = NewRole[0];

            await db.SaveChangesAsync();

            return View();
        }//end of ChangeToPromotor

        public async Task<ActionResult> ChangeToBoxer(string UserName)
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
            var ResultRoleChange = await UserManager.AddToRoleAsync(CurrentUser.Id, "Boxer");

            var NewRole = await UserManager.GetRolesAsync(CurrentUser.Id);
            ViewBag.NewUserRole = NewRole[0];

            await db.SaveChangesAsync();

            return View();
        }//end of ChangeToBoxer

        public async Task<ActionResult> ChangeToFan(string UserName)
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
            var ResultRoleChange = await UserManager.AddToRoleAsync(CurrentUser.Id, "Fan");

            var NewRole = await UserManager.GetRolesAsync(CurrentUser.Id);
            ViewBag.NewUserRole = NewRole[0];

            await db.SaveChangesAsync();

            return View();
        }//end of ChangeToFan

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

        #endregion
    }


}