using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LAPP.ENTITY;
using LAPP.BAL;

namespace LAPP.WS.App_Helper.Common
{
    public class LoginHelper
    {
        public static void SaveLoginHistory(Users objUser, string Key = "")
        {
            try
            {
                if (HttpContext.Current != null)
                {

                    UserLoginHistory objLoginHist = new UserLoginHistory();
                    UserLoginHistoryBAL objLoginHistBAL = new UserLoginHistoryBAL();
                    objLoginHist.Email = objUser.Email;
                    objLoginHist.LoginDate = DateTime.Now;
                    objLoginHist.LoginIp = HttpContext.Current.Request.UserHostAddress;
                    objLoginHist.MachineName = "";
                    objLoginHist.IndividualId = objUser.IndividualId;
                    objLoginHist.UserAgent = HttpContext.Current.Request.UserAgent;
                    objLoginHist.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                    objLoginHist.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                    objLoginHist.UserId = objUser.UserId;
                    objLoginHist.UserName = objUser.UserName;
                    objLoginHist.UserLoginHistoryGuid = Convert.ToString(Guid.NewGuid());
                    objLoginHistBAL.Save_UserLoginHistory(objLoginHist);
                }
            }
            catch (Exception ex) { LogingHelper.SaveExceptionInfo(Key, ex, "SavePasswordChangedHistory", ENTITY.Enumeration.eSeverity.Critical); }
        }


        public static void SaveLogOutHistory(int UserId, string Key)
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    UsersBAL objUserBAL = new UsersBAL();
                    Users objUser = objUserBAL.Get_Users_byUsersId(UserId);
                    if (objUser != null)
                    {
                        UserLoginHistory objLoginHist = new UserLoginHistory();
                        UserLoginHistoryBAL objLoginHistBAL = new UserLoginHistoryBAL();
                        objLoginHist.Email = objUser.Email;
                        objLoginHist.LogoutDate = DateTime.Now;
                        objLoginHist.LoginIp = HttpContext.Current.Request.UserHostAddress;
                        objLoginHist.MachineName = "";
                        objLoginHist.IndividualId = objUser.IndividualId;
                        objLoginHist.UserAgent = HttpContext.Current.Request.UserAgent;
                        objLoginHist.UserHostName = HttpContext.Current.Request.UserHostName;
                        objLoginHist.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                        objLoginHist.UserId = objUser.UserId;
                        objLoginHist.UserName = objUser.UserName;
                        objLoginHist.UserLoginHistoryGuid = Convert.ToString(Guid.NewGuid());
                        objLoginHistBAL.Save_UserLoginHistory(objLoginHist);
                    }
                }
            }
            catch (Exception ex) { LogingHelper.SaveExceptionInfo(Key, ex, "SavePasswordChangedHistory", ENTITY.Enumeration.eSeverity.Critical); }
        }


        public static void SavePasswordChangedHistory(Users objUser, string Key="")
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    UserLoginHistory objLoginHist = new UserLoginHistory();
                    UserLoginHistoryBAL objLoginHistBAL = new UserLoginHistoryBAL();
                    objLoginHist.Email = objUser.Email;
                    objLoginHist.PasswordChangedOn = DateTime.Now;
                    objLoginHist.LoginIp = HttpContext.Current.Request.UserHostAddress;
                    objLoginHist.MachineName = "";
                    objLoginHist.UserAgent = HttpContext.Current.Request.UserAgent;
                    objLoginHist.UserHostName = HttpContext.Current.Request.UserHostName;
                    objLoginHist.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                    objLoginHist.UserId = objUser.UserId;
                    objLoginHist.IndividualId = objUser.IndividualId;
                    objLoginHist.UserName = objUser.UserName;
                    objLoginHist.UserLoginHistoryGuid = Convert.ToString(Guid.NewGuid());
                    objLoginHistBAL.Save_UserLoginHistory(objLoginHist);
                }
            }
            catch (Exception ex) { LogingHelper.SaveExceptionInfo(Key, ex, "SavePasswordChangedHistory", ENTITY.Enumeration.eSeverity.Critical); }
        }
    }
}