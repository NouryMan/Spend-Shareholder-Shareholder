using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shareholder_System.Startup))]
namespace Shareholder_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
