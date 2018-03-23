namespace Noah.Web.Models
{
  public class ChatModel
  {
    public string ServerUrl { get; set; }

    public string ServerToken { get; set; }

    public string ServerHubUrl => ServerUrl + "/hubs";
  }
}
