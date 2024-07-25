using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Spend_Web_System.Startup))]
namespace Spend_Web_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
