using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryManagementProject.Startup))]
namespace LibraryManagementProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
