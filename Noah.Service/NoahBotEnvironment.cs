using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Noah.Common;
using ZimmerBot.Core;

namespace Noah.Service
{
  public class NoahBotEnvironment : IBotEnvironment
  {
    static ILog Logger = LogManager.GetLogger(typeof(NoahBotEnvironment));


    public void HandleResponse(Response response)
    {
      try
      {
        foreach (string output in response.Output)
        {
          NewMessageArgs args = new NewMessageArgs
          {
            Timestamp = DateTime.Now,
            Name = "Noah",
            Message = output
          };
          NoahServer.Instance.SendMessage(args);
        }
      }
      catch (Exception ex)
      {
        Logger.Error(ex);
      }
    }


    public void Log(LogLevel level, string msg, params object[] args)
    {
      switch (level)
      {
        case LogLevel.Debug:
          Logger.DebugFormat(msg, args);
          break;
        case LogLevel.Info:
          Logger.InfoFormat(msg, args);
          break;
        case LogLevel.Warn:
          Logger.WarnFormat(msg, args);
          break;
        case LogLevel.Error:
          Console.WriteLine("Internal error");
          Logger.ErrorFormat(msg, args);
          break;
      }
    }


    public void Log(LogLevel level, string msg, Exception ex)
    {
      switch (level)
      {
        case LogLevel.Debug:
          Logger.Debug(msg, ex);
          break;
        case LogLevel.Info:
          Logger.Info(msg, ex);
          break;
        case LogLevel.Warn:
          Logger.Warn(msg, ex);
          break;
        case LogLevel.Error:
          Console.WriteLine("Internal error");
          Logger.Error(msg, ex);
          break;
      }
    }
  }
}
