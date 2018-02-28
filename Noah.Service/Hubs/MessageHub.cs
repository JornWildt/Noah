using Microsoft.AspNet.SignalR;
using Noah.Common;
using ZimmerBot.Core;

namespace Noah.Service.Hubs
{
  public class MessageHub : Hub<IClientContract>, IServerHub
  {
    public void Say(string text)
    {
      NoahService.NoahBotHandle.Invoke(new Request { Input = text });
    }
  }
}
