using Elfisk.Commons;

namespace Noah.Web
{
  public static class WebAppSettings
  {
    public static readonly AppSetting<string> ServerUrl = new AppSetting<string>("Web.ServerUrl");
  }
}
