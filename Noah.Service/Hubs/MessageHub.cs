using Microsoft.AspNet.SignalR;
using ZimmerBot.Core;

namespace Noah.Service.Hubs
{
  public class MessageHub : Hub
  {
    public void Say(string text)
    {
      NoahService.NoahBotHandle.Invoke(new Request { Input = text });
    }
  }
}
