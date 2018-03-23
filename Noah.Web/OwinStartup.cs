using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using log4net.Config;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Noah.Web.OwinStartup))]

namespace Noah.Web
{
  public class OwinStartup
  {
    static readonly ILog Logger = LogManager.GetLogger((typeof(OwinStartup)));

    public void Configuration(IAppBuilder app)
    {
      XmlConfigurator.Configure();

      Logger.Info("**************************************************************************************");
      Logger.Info("Starting Noah web (OWIN)");
      Logger.Info("**************************************************************************************");

      ConfigureAuth(app);

      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }


    public void ConfigureAuth(IAppBuilder app)
    {
      // Enable the application to use a cookie to store information for the signed in user
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login")
      });


      app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

      /* Remember
       * - Use HTTPS
       * - Set BOTH allowed return URL AND allowed javascript server/host in G-application.
       * - Enable Google+ API in G-application as the authentication accesses it for further user information.
       */

      app.UseGoogleAuthentication(
          clientId: WebAppSettings.GoogleClientID,
          clientSecret: WebAppSettings.GoogleClientSecret);

      AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
    }
  }
}
