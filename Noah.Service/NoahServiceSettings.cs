using System;
using Elfisk.Commons;

namespace Noah.Service
{
  public static class NoahServiceSettings
  {
    public static readonly AppSetting<TimeSpan> GameLoopPeriod = new AppSetting<TimeSpan>("Noah.GameLoopPeriod", TimeSpan.FromSeconds(1));
    public static readonly AppSetting<string> BotSource = new AppSetting<string>("Noah.BotSource");
  }
}
