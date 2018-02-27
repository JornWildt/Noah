using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;

namespace Noah.Service
{
  public class OwinStartup : Elfisk.ECS.Service.OwinStartup
  {
    public override void Configuration(IAppBuilder app)
    {
      base.Configuration(app);

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
