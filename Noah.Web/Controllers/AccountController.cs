using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Noah.Web.Utilities;

namespace Noah.Web.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private static ILog Logger = LogManager.GetLogger(typeof(AccountController));

    /// <summary>
    /// Show login page.
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }


    /// <summary>
    /// Handle external login request (from login button).
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult ExternalLogin(string provider, string returnUrl)
    {
      // Request a redirect to the external login provider
      return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
    }


    // GET: /Account/ExternalLoginCallback
    [AllowAnonymous]
    public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
    {
      Logger.Debug("External login callback.");

      ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
      if (loginInfo == null)
      {
        Logger.Debug("No login info found - redirecting to login page again.");
        return RedirectToAction("Login");
      }

      string name = loginInfo.ExternalIdentity.Name;
      string email = loginInfo.Email;
      string userId = loginInfo.Login.LoginProvider + "-" + loginInfo.ExternalIdentity.FindFirstValue(ClaimTypes.NameIdentifier);

      Logger.Debug($"Logged in as '{name}' - '{email}' ({userId}).");

      var claims = new List<Claim>();

      // create required claims
      claims.Add(new Claim(ClaimTypes.Email, email));
      claims.Add(new Claim(ClaimTypes.Name, name));
      claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
      var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

      AuthenticationManager.SignIn(new AuthenticationProperties()
      {
        AllowRefresh = true,
        IsPersistent = true,
        ExpiresUtc = DateTime.UtcNow.AddDays(7)
      }, identity);

      Logger.Debug($"Redirect to '{returnUrl}'.");
      return RedirectToLocal(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Logout()
    {
      AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);

      return RedirectToAction("Login");
    }

    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }


    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      return RedirectToAction("Index", "Chat");
    }
  }
}
