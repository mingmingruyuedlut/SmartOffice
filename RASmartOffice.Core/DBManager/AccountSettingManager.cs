using RASmartOffice.Common;
using RASmartOffice.Core.Context;
using RASmartOffice.Core.Entities;
using RASmartOffice.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RASmartOffice.Core.DBManager
{
    public class AccountSettingManager
    {
        private RASOConext _context = new RASOConext();

        public ChangePasswordValidateResult ChangeAccountPassword(int userId, string oldPwd, string newPwd)
        {
            ChangePasswordValidateResult vResult = new ChangePasswordValidateResult();

            if (!CheckOldPassword(userId, oldPwd))
            {
                vResult = new ChangePasswordValidateResult() { ValidateResultType = ChangePasswordValidateResultType.OldPasswordInvalid, Message = "原始密码输入错误" };
            }
            else
            {
                ChangeAccountPassword(userId, newPwd);
                vResult = new ChangePasswordValidateResult() { ValidateResultType = ChangePasswordValidateResultType.Success, Message = "修改密码成功" };
            }

            return vResult;
        }

        private void ChangeAccountPassword(int userId, string newPwd)
        {
            User user = _context.User.Find(userId);
            if (user != null)
            {
                user.Password = PasswordUtilities.Encrypt(newPwd);
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public bool CheckOldPassword(int userId, string oldPwd)
        {
            string encryptedPwd = _context.User.Find(userId).Password;
            return PasswordUtilities.ValidatePassword(oldPwd, encryptedPwd);
        }


        public AccountInfo GetAccountInfo(int userId)
        {
            return _context.User.Where(x => x.ID == userId).Select(x => new AccountInfo
            {
                UserId = x.ID,
                UserName = x.UserName,
                DisplayName = x.DisplayName,
                Phone = x.Phone,
                Email = x.Email,
                Notes = x.Notes,
                CreateTime = x.CreateTime,
                UpdateTime = x.UpdateTime
            }).FirstOrDefault();
        }

        public List<SelectListItem> GetUsersDropDownList(int roleId)
        {
            List<int> userIds = _context.UserRole.Where(x => x.RoleID == roleId).Select(x => x.UserID).Distinct().ToList();

            return _context.User
                .Where(x => userIds.Contains(x.ID))
                .Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = string.IsNullOrEmpty(x.UserName.Trim()) ? x.UserName : x.UserName + "-" + x.DisplayName.Trim()
                }).ToList();
        }

        public void UpdateAccountInfo(AccountInfo account)
        {
            User currentUser = _context.User.Where(x => x.ID == account.UserId).FirstOrDefault();
            currentUser.DisplayName = account.DisplayName;
            currentUser.Email = account.Email;
            currentUser.Phone = account.Phone;
            currentUser.Notes = account.Notes;

            _context.Entry(currentUser).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool CheckEmailIsValid(string email, int userId)
        {
            return !_context.User.Any(x => x.Email == email && x.ID != userId);
        }

        public bool CheckPhoneIsValid(string phone, int userId)
        {
            return !_context.User.Any(x => x.Phone == phone && x.ID != userId);
        }

        public bool CheckAdminAccountUserName(string username, int userId)
        {
            return !_context.User.Any(x => x.UserName == username && x.ID != userId);
        }
    }
}
