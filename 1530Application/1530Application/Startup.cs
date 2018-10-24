using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_1530Application.Startup))]
namespace _1530Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            DbConnection dbConnection = new DbConnection();
        }
    }
}
