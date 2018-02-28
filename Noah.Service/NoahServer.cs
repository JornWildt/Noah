using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Noah.Common;
using Noah.Service.Hubs;

namespace Noah.Service
{
  public class NoahServer
  {
    // Singleton instance
    private readonly static Lazy<NoahServer> _instance = new Lazy<NoahServer>(() => new NoahServer(GlobalHost.ConnectionManager.GetHubContext<MessageHub,IClientContract>().Clients));

    public static NoahServer Instance
    {
      get { return _instance.Value; }
    }

    private IHubConnectionContext<IClientContract> Clients { get; set; }


    public NoahServer(IHubConnectionContext<IClientContract> clients)
    {
      Clients = clients;
    }


    public void SendMessage(string text)
    {
      Instance.Clients.All.NewMessage(text);
    }
  }
}
