using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektASP.Startup))]
namespace ProjektASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
