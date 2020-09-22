using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_19Jul2020_1.Startup))]
namespace _19Jul2020_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
