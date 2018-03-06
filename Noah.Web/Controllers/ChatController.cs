using System.Web.Mvc;
using Noah.Web.Models;

namespace Noah.Web.Controllers
{
  public class ChatController : Controller
  {
    public ActionResult Index()
    {
      ChatModel model = new ChatModel
      {
        ServerUrl = WebAppSettings.ServerUrl.Value
      };

      return View(model);
    }
  }
}