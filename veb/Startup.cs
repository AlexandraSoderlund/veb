using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(veb.Startup))]
namespace veb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
