using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Documentoobig.Startup))]
namespace Documentoobig
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
