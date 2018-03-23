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

        // Some people report problems with OWIN and ASP.NET Cookie handling. Try this.
        //CookieManager = new SystemWebChunkingCookieManager()
      });


      app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

      app.UseGoogleAuthentication(
          clientId: "",
          clientSecret: "");

      /* Also remember
       * - Set BOTH allowed return URL AND allowed javascript server/host in G-application.
       * - Enable Google+ API in G-application.
       */

      // App.Secrets is application specific and holds values in CodePasteKeys.json
      // Values are NOT included in repro – auto-created on first load
      //if (!string.IsNullOrEmpty(App.Secrets.GoogleClientId))
      //{
      //  app.UseGoogleAuthentication(
      //      clientId: App.Secrets.GoogleClientId,
      //      clientSecret: App.Secrets.GoogleClientSecret);
      //}

      //if (!string.IsNullOrEmpty(App.Secrets.TwitterConsumerKey))
      //{
      //  app.UseTwitterAuthentication(
      //      consumerKey: App.Secrets.TwitterConsumerKey,
      //      consumerSecret: App.Secrets.TwitterConsumerSecret);
      //}

      //if (!string.IsNullOrEmpty(App.Secrets.GitHubClientId))
      //{
      //  app.UseGitHubAuthentication(
      //      clientId: App.Secrets.GitHubClientId,
      //      clientSecret: App.Secrets.GitHubClientSecret);
      //}

      AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
    }
  }
}
