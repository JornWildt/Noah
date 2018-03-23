using Microsoft.AspNet.SignalR;
using Noah.Service.Hubs;
using Owin;

namespace Noah.Service
{
  public class OwinStartup : Elfisk.ECS.Service.OwinStartup
  {
    public override void Configuration(IAppBuilder app)
    {
      base.Configuration(app);

      GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());

      //// Configure Web API for self-host. 
      //HttpConfiguration config = new HttpConfiguration();
      //config.Routes.MapHttpRoute(
      //    name: "DefaultApi",
      //    routeTemplate: "api/{controller}/{id}",
      //    defaults: new { id = RouteParameter.Optional }
      //);

      //var xml = config.Formatters.XmlFormatter;
      //xml.UseXmlSerializer = true;

      //app.UseWebApi(config);
    }
  }
}
