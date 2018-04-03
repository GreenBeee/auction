using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewAuction.Startup))]
namespace NewAuction
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
