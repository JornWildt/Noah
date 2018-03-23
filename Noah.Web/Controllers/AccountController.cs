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

namespace Noah.Web.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private static ILog Logger = LogManager.GetLogger(typeof(AccountController));

    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult ExternalLogin(string provider, string returnUrl)
    {
      ControllerContext.HttpContext.Session.RemoveAll();
      //Session["Workaround"] = 0;

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

      Logger.Debug($"Logged in as '{loginInfo.Email}'.");

      var claims = new List<Claim>();

      // create required claims
      claims.Add(new Claim(ClaimTypes.NameIdentifier, loginInfo.Email));
      claims.Add(new Claim(ClaimTypes.Name, loginInfo.DefaultUserName));
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
