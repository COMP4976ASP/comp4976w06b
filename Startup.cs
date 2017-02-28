using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab6b.Startup))]
namespace Lab6b
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
