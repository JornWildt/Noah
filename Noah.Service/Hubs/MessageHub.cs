using System;
using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.IdentityModel.Tokens;
using Noah.Common;
using Noah.Service.Utilities;
using ZimmerBot.Core;

namespace Noah.Service.Hubs
{
  public class MessageHub : Hub<IClientContract>, IServerHub
  {
    private static ILog Logger = LogManager.GetLogger(typeof(MessageHub));

    public void Say(string token, string text)
    {
      try
      {
        string userId = SecurityUtilities.VerifyToken(token);

        if (NoahService.NoahBotHandle != null)
        {
          NoahService.NoahBotHandle.Invoke(new Request
          {
            Input = text,
            SessionId = userId,
            UserId = userId
          });
        }
      }
      catch (SecurityTokenInvalidSignatureException ex)
      {
        Logger.Error(ex);
        throw new HubException("Ingen adgang.");
      }
      catch (Exception ex)
      {
        Logger.Error(ex);
        throw new HubException(ex.Message);
      }
    }
  }
}
