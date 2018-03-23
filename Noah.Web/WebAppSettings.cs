using Elfisk.Commons;

namespace Noah.Web
{
  public static class WebAppSettings
  {
    public static readonly AppSetting<string> ServerUrl = new AppSetting<string>("Web.ServerUrl");
    public static readonly AppSetting<string> ServiceSecret = new AppSetting<string>("Web.ServiceSecret");

    public static readonly AppSetting<string> GoogleClientID = new AppSetting<string>("Web.GoogleClientID");
    public static readonly AppSetting<string> GoogleClientSecret = new AppSetting<string>("Web.GoogleClientSecret");
  }
}
