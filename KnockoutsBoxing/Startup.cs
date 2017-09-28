using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KnockoutsBoxing.Startup))]
namespace KnockoutsBoxing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
