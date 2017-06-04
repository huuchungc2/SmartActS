using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartActS.Startup))]
namespace SmartActS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
