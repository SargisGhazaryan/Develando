using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Develando.Web.Startup))]
namespace Develando.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
