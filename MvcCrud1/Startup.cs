using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcCrud1.Startup))]
namespace MvcCrud1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
