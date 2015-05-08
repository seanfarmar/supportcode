using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProblemTest.Startup))]
namespace ProblemTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
