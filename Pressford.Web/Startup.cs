using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pressford.Web.Startup))]
namespace Pressford.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
