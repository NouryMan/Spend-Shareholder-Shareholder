using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Contributions_System.Startup))]
namespace Contributions_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
