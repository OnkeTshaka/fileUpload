using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Upload.Startup))]
namespace Upload
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
