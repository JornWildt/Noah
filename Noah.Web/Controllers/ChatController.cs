using System.Web;
using System.Web.Mvc;
using Noah.Web.Models;
using Noah.Web.Utilities;

namespace Noah.Web.Controllers
{
  public class ChatController : Controller
  {
    [Authorize]
    public ActionResult Index()
    {
      ChatModel model = new ChatModel
      {
        ServerUrl = WebAppSettings.ServerUrl.Value,
        ServerToken = SecurityUtilities.GenerateServerToken(HttpContext.GetOwinContext().Authentication),
        UserName = new MvcHtmlString(JavaScriptStringEncode(User.Identity.Name))
      };

      return View(model);
    }

    string JavaScriptStringEncode(string s)
    {
      if (s == null)
        return s;
      return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
  }
}