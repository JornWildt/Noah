using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace Noah.Web.Controllers
{
  internal class ChallengeResult : HttpUnauthorizedResult
  {
    public ChallengeResult(string provider, string redirectUri)
        : this(provider, redirectUri, null)
    {
    }

    public ChallengeResult(string provider, string redirectUri, string userId)
    {
      LoginProvider = provider;
      RedirectUri = redirectUri;
      UserId = userId;
    }

    public string LoginProvider { get; set; }
    public string RedirectUri { get; set; }
    public string UserId { get; set; }

    // Used for XSRF protection when adding external logins
    private const string XsrfKey = "XsrfId";

    public override void ExecuteResult(ControllerContext context)
    {
      var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
      if (UserId != null)
      {
        properties.Dictionary[XsrfKey] = UserId;
      }
      context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
    }
  }
}
