namespace KnockoutsBoxing.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<KnockoutsBoxing.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KnockoutsBoxing.Models.ApplicationDbContext context)
        {
            AddUserandRole(context);
        }

        private void AddUserandRole(ApplicationDbContext context)
        {
            //this is where I add roles to the Database

            IdentityResult ir;

            //var rm1 = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //ir = rm1.Create(new IdentityRole("canEdits"));

            var rolemanage1 = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rolemanage1.Create(new IdentityRole("Fan"));
            ir = rolemanage1.Create(new IdentityRole("Promoter"));
            ir = rolemanage1.Create(new IdentityRole("Boxer"));
            ir = rolemanage1.Create(new IdentityRole("Moderator"));
            ir = rolemanage1.Create(new IdentityRole("Administrator"));

        }
    }
}
