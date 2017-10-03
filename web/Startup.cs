using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Entt.Ers.Startup))]
namespace Entt.Ers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
