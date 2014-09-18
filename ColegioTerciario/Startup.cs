using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ColegioTerciario.Startup))]
namespace ColegioTerciario
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
