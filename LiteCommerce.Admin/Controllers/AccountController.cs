using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Account
        public ActionResult Login(string loginName = "", string password = "")
        {
            ViewBag.LoginName = loginName;

            if (Request.HttpMethod == "POST")
            {
                var account = AccountService.Authorize(loginName, CryptHelper.Md5(password));
                if (account == null)
                {
                    ModelState.AddModelError("", "thông tin đăng nhập bị sai");
                    return View();
                }
                var photoLink = PhotoLinkHelper.photoLink(AccountService.getEmployeePhotoByEmail(loginName));

                if (string.IsNullOrEmpty(photoLink))
                {
                    photoLink = "";
                }
                FormsAuthentication.SetAuthCookie(CookieHelper.AccountToCookieString(account), false);
                Session.Add("photoLink", photoLink);
                Session.Add("fullName", account.FullName);
                Session.Add("loginName", loginName);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult ChangePassword(string oldPassword = "", string newPassword = "", string confirmPassword = "")
        {
            if (Session["loginName"] != null)
            {
                if (Request.HttpMethod == "POST")
                {
                    ViewBag.OldPassword = oldPassword;
                    ViewBag.NewPassword = newPassword;
                    if (string.IsNullOrEmpty(newPassword))
                    {
                        newPassword = "123456";
                    }
                    var password = AccountService.getOldPaswword(Session["loginName"].ToString());
                    if (!CryptHelper.Md5(oldPassword).Equals(password.ToUpper()))
                    {
                        ModelState.AddModelError("", "mật khẩu hiện tại bị sai");
                        return View();
                    }
                    else
                    {
                        if(AccountService.ChangePassword(Session["loginName"].ToString(), password, CryptHelper.Md5(newPassword).ToLower()))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Content("Ối giồi ôi");
                        }
                    }
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}