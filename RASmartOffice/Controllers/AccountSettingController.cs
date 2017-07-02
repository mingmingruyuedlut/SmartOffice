using Newtonsoft.Json;
using RASmartOffice.Core.DBManager;
using RASmartOffice.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RASmartOffice.Controllers
{
    public class AccountSettingController : Controller
    {
        // GET: AccountSetting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            LoginUser loginUser = (LoginUser)Session["CurrentLoginUser"];
            ChangePasswordInfo changePwdInfo = new ChangePasswordInfo() { UserId = loginUser.UserId, UserName = loginUser.UserName };
            return View(changePwdInfo);
        }

        public JsonResult ChangeAccountPassword(string accountPwdJsonStr)
        {
            ChangePasswordInfo pwdInfo = JsonConvert.DeserializeObject<ChangePasswordInfo>(accountPwdJsonStr);
            ChangePasswordValidateResult vResult = new AccountSettingManager().ChangeAccountPassword(pwdInfo.UserId, pwdInfo.OldPassword, pwdInfo.NewPassword);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckOldPassword(int userId, string oldPwd)
        {
            bool vResult = new AccountSettingManager().CheckOldPassword(userId, oldPwd);
            return Json(vResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PersonalInformation()
        {
            LoginUser loginUser = (LoginUser)Session["CurrentLoginUser"];
            AccountInfo account = new AccountSettingManager().GetAccountInfo(loginUser.UserId);
            return View(account);
        }

        public JsonResult SaveAccountInformation(string accountJsonStr)
        {
            AccountInfo account = JsonConvert.DeserializeObject<AccountInfo>(accountJsonStr);
            new AccountSettingManager().UpdateAccountInfo(account);
            Session["CurrentLoginUser"] = new AccountManager().GetLoginUser(account.UserId);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckEmailIsValid(string email, int userId)
        {
            bool vResult = new AccountSettingManager().CheckEmailIsValid(email, userId);
            return Json(vResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckPhoneIsValid(string phone, int userId)
        {
            bool vResult = new AccountSettingManager().CheckPhoneIsValid(phone, userId);
            return Json(vResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckAccountUserName(string username, int userId)
        {
            bool vResult = new AccountSettingManager().CheckAdminAccountUserName(username, userId);
            return Json(vResult, JsonRequestBehavior.AllowGet);
        }
    }
}