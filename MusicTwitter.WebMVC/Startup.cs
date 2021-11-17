using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicTwitter.WebMVC.Startup))]
namespace MusicTwitter.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
