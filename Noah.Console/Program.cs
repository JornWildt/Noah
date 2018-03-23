using System;
using System.Threading.Tasks;
using Elfisk.Commons;
using Microsoft.AspNet.SignalR.Client;
using Noah.Common;

namespace Noah.Console
{
  class Program
  {
    static AppSetting<string> ClientUrl = new AppSetting<string>("Client.SignalRUrl");

    static HubConnection ServerConnection { get; set; }

    static IHubProxy<IServerHub,IClientContract> MessageHub { get; set; }


    static void Main(string[] args)
    {
      AsyncMain().GetAwaiter().GetResult();
    }


    static async Task AsyncMain()
    {
      await Initialize();
      Run();
      Shutdown();
    }


    static async Task Initialize()
    {
      ServerConnection = new HubConnection(ClientUrl);
      MessageHub = ServerConnection.CreateHubProxy<IServerHub, IClientContract>("MessageHub");
      MessageHub.SubscribeOn<NewMessageArgs>(hub => hub.NewMessage, HandleNewMessage);
      await ServerConnection.Start();
    }


    static void Run()
    {
      string input;

      System.Console.Write("> ");

      do
      {
        input = System.Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
          MessageHub.Call(hub => hub.Say("X", input));
        }
      }
      while (!string.IsNullOrEmpty(input));
    }


    static void HandleNewMessage(NewMessageArgs args)
    {
      System.Console.WriteLine($"[{args.Timestamp}] {args.Name} > {args.Message}");
      System.Console.Write("> ");
    }


    static void Shutdown()
    {
      ServerConnection.Stop();
      ServerConnection.Dispose();
    }
  }
}
