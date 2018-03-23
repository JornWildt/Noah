using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noah.Web.App_Start
{
  public static class AuthConfig
  {
    public static void RegisterAuth()
    {
      //OAuthWebSecurity.RegisterMicrosoftClient(
      //    clientId: "",
      //    clientSecret: "");

      //OAuthWebSecurity.RegisterTwitterClient(
      //    consumerKey: "",
      //    consumerSecret: "");

      Microsoft.Web.
      OAuthWebSecurity.RegisterFacebookClient(
          appId: "",
          appSecret: "");

      //OAuthWebSecurity.RegisterGoogleClient();        
    }
  }
}
