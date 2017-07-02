using RASmartOffice.Common;
using RASmartOffice.Core.Context;
using RASmartOffice.Core.Entities;
using RASmartOffice.Core.Enums;
using RASmartOffice.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASmartOffice.Core.DBManager
{
    public class AccountManager
    {
        private RASOConext _context = new RASOConext();


        /// <summary>
        /// Validate account sign in
        /// </summary>
        /// <param name="username">it's login name</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginUser AccountSignInValidation(string username, string password)
        {
            LoginUser currentUser = new LoginUser();
            User user = _context.User.FirstOrDefault(x => x.UserName == username);
            if (user != null && user.UserName != null)
            {
                if (!string.IsNullOrEmpty(password) && PasswordUtilities.ValidatePassword(password, user.Password))
                {
                    currentUser.UserId = user.ID;
                    currentUser.UserName = user.UserName;
                    currentUser.DisplayName = user.DisplayName;
                    currentUser.RoleType = RoleType.user;
                    List<int> roleIds = _context.UserRole.Where(x => x.UserID == user.ID).Select(x => x.RoleID).ToList();
                    if (roleIds.Contains(RoleType.admin))
                    {
                        currentUser.RoleType = RoleType.admin;
                    }

                    currentUser.ValidateResult = new LoginUserValidateResult() { ValidateResultType = LoginUserValidateResultType.Success, Message = "Login successful." };
                }
                else
                {
                    currentUser.ValidateResult = new LoginUserValidateResult() { ValidateResultType = LoginUserValidateResultType.PasswordInvalid, Message = "Password is invalid." };
                }
            }
            else
            {
                currentUser.ValidateResult = new LoginUserValidateResult() { ValidateResultType = LoginUserValidateResultType.UserNotExist, Message = "User is not exist." };
            }

            return currentUser;
        }

        public LoginUser GetLoginUser(int userId)
        {
            LoginUser currentUser = new LoginUser();
            User user = _context.User.FirstOrDefault(x => x.ID == userId);
            if (user != null && user.UserName != null)
            {
                currentUser.UserId = user.ID;
                currentUser.UserName = user.UserName;
                currentUser.DisplayName = user.DisplayName;
                currentUser.RoleType = RoleType.user;
                List<int> roleIds = _context.UserRole.Where(x => x.UserID == user.ID).Select(x => x.RoleID).ToList();
                if (roleIds.Contains(RoleType.admin))
                {
                    currentUser.RoleType = RoleType.admin;
                }
                currentUser.ValidateResult = new LoginUserValidateResult() { ValidateResultType = LoginUserValidateResultType.Success, Message = "Login successful." };
            }
            else
            {
                currentUser.ValidateResult = new LoginUserValidateResult() { ValidateResultType = LoginUserValidateResultType.UserNotExist, Message = "User is not exist." };
            }

            return currentUser;
        }

        public RegisterInfo AccountRegister(RegisterInfo userInfo)
        {
            if (_context.User.Count(x => x.UserName == userInfo.UserName) > 0)
            {
                userInfo.ValidateResult = new RegisterValidateResult() { ValidateResultType = RegisterValidateResultType.UserIsExist, Message = "User is exist, please change the user name and try again." };
            }
            else
            {
                if (!string.IsNullOrEmpty(userInfo.Password) && !string.IsNullOrEmpty(userInfo.ConfirmedNewPassword) && userInfo.Password.Equals(userInfo.ConfirmedNewPassword))
                {
                    User currentUser = new User()
                    {
                        UserName = userInfo.UserName,
                        Password = PasswordUtilities.Encrypt(userInfo.Password),
                        Phone = userInfo.Phone,
                        Email = userInfo.Email,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };

                    _context.User.Add(currentUser);
                    _context.SaveChanges();

                    AddUserRole(userInfo);

                    userInfo.ValidateResult = new RegisterValidateResult() { ValidateResultType = RegisterValidateResultType.Success, Message = "Register successful." };
                }
                else
                {
                    userInfo.ValidateResult = new RegisterValidateResult() { ValidateResultType = RegisterValidateResultType.InvalidPassword, Message = "Invalid password." };
                }
            }

            return userInfo;
        }

        public bool AddUserRole(RegisterInfo userInfo)
        {
            bool valResult = true;
            try
            {
                int userId = _context.User.FirstOrDefault(x => x.UserName == userInfo.UserName).ID;
                UserRole ur = new UserRole()
                {
                    RoleID = RoleType.user,
                    UserID = userId
                };
                _context.UserRole.Add(ur);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return valResult;
        }
    }
}
