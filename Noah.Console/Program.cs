using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elfisk.Commons;
using Microsoft.AspNet.SignalR.Client;

namespace Noah.Console
{
  class Program
  {
    static AppSetting<string> ClientUrl = new AppSetting<string>("Client.SignalRUrl");

    static HubConnection ServerConnection { get; set; }

    static IHubProxy MessageHub { get; set; }


    static void Main(string[] args)
    {
      ConsoleApp().GetAwaiter().GetResult();
    }


    static async Task ConsoleApp()
    {
      await Initialize();
      await Run();
      Shutdown();
    }


    static async Task Initialize()
    {
      ServerConnection = new HubConnection(ClientUrl);
      MessageHub = ServerConnection.CreateHubProxy("MessageHub");

      MessageHub.On<string>("NewMessage", HandleNewMessage);
      await ServerConnection.Start();
    }


    static async Task Run()
    {
      string input;

      System.Console.Write("> ");

      do
      {
        input = System.Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
          await MessageHub.Invoke("Say", input);
        }
      }
      while (!string.IsNullOrEmpty(input));
    }


    static void Shutdown()
    {
      ServerConnection.Stop();
      ServerConnection.Dispose();
    }


    static void HandleNewMessage(string text)
    {
      System.Console.WriteLine(text);
      System.Console.Write("> ");
    }
  }
}
