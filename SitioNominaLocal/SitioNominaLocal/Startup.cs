using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SitioNominaLocal.Startup))]
namespace SitioNominaLocal
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            //ConfigureAuth(app);
        }
    }
}
