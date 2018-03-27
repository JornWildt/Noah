using System.Web.Mvc;

namespace Noah.Web.Models
{
  public class ChatModel
  {
    public string ServerUrl { get; set; }

    public string ServerToken { get; set; }

    public MvcHtmlString UserName { get; set; }

    public string ServerHubUrl => ServerUrl + "/hubs";
  }
}
