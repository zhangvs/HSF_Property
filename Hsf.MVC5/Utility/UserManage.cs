﻿using Hsf.Bussiness.Interface;
using Hsf.EF.Model;
using Hsf.Framework.Encrypt;
using Hsf.Framework.Extension;
using Hsf.Framework.Log;
using Hsf.Web.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace Hsf.MVC5.Utility
{
    /// <summary>
    /// 用户登录/退出
    /// </summary>
    public static class UserManage
    {
        private static Logger logger = Logger.CreateLogger(typeof(UserManage));

        /// <summary>
        /// 0成功 1用户不存在 2密码错误 3 验证码错误 4账号已冻结
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="verify"></param>
        /// <returns></returns>
        public static LoginResult UserLogin(this HttpContextBase context, string name = "", string pwd = "", string verify = "")
        {
            if (string.IsNullOrEmpty(verify) || context.Session["CheckCode"] == null || !context.Session["CheckCode"].ToString().Equals(verify, StringComparison.OrdinalIgnoreCase))
            {
                return LoginResult.WrongVerify;
            }

            //IUserMenuService service = DIFactory.GetContainer().Resolve<IUserMenuService>();
            IUserMenuService service = IOCConfig.IOCContainer.Resolve<IUserMenuService>();
            User user = service.UserLogin(name);
            if (user == null)
            {
                return LoginResult.NoUser;
            }
            else if (!user.Password.Equals(MD5Encrypt.Encrypt(pwd)))
            {
                return LoginResult.WrongPwd;
            }
            else if (user.State == (int)UserState.Frozen)
            {
                return LoginResult.Frozen;
            }
            else
            {
                //Response，Request，Application，Server，Session

                #region Server
                //辅助类 Server
                string encode = context.Server.HtmlEncode("<我爱我家>");
                string decode = context.Server.HtmlDecode(encode);
                string physicalPath = context.Server.MapPath("/home/index");//只能做物理文件的映射
                string encodeUrl = context.Server.UrlEncode("<我爱我家>");
                string decodeUrl = context.Server.UrlDecode(encodeUrl);
                #endregion

                #region Application
                context.Application.Lock();//ASP.NET 应用程序内的多个会话和请求之间共享信息
                context.Application.Lock();
                context.Application.Add("try", "die");
                context.Application.UnLock();
                object aValue = context.Application.Get("try");
                aValue = context.Application["try"];
                context.Application.Remove("命名对象");
                context.Application.RemoveAt(0);
                context.Application.RemoveAll();
                context.Application.Clear();

                context.Items["123"] = "123";//单一会话，不同环境都可以用
                #endregion

                #region Cookie
                CurrentUser currentUser = new CurrentUser()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Account = user.Account,
                    Email = user.Email,
                    Password = user.Password,
                    LoginTime = DateTime.Now
                };
                //HttpCookie cookie = context.Request.Cookies.Get("CurrentUser");
                //if (cookie == null)
                //{
                HttpCookie myCookie = new HttpCookie("CurrentUser");
                myCookie.Value = JsonHelper.ObjectToString<CurrentUser>(currentUser);
                myCookie.Expires = DateTime.Now.AddMinutes(5);
                context.Response.Cookies.Add(myCookie);
                //}
                #endregion Cookie

                #region Session
                //context.Session.RemoveAll();
                var sessionUser = context.Session["CurrentUser"];

                context.Session["CurrentUser"] = currentUser;
                context.Session.Timeout = 3;//minute  session过期等于Abandon
                #endregion Session


                logger.Debug(string.Format("用户id={0} Name={1}登录系统", currentUser.Id, currentUser.Name));
                service.LastLogin(user);
                return LoginResult.Success;
            }
        }

        public enum LoginResult
        {
            /// <summary>
            /// 登录成功
            /// </summary>
            [RemarkAttribute("登录成功")]
            Success = 0,
            /// <summary>
            /// 用户不存在
            /// </summary>
            [RemarkAttribute("用户不存在")]
            NoUser = 1,
            /// <summary>
            /// 密码错误
            /// </summary>
            [RemarkAttribute("密码错误")]
            WrongPwd = 2,
            /// <summary>
            /// 验证码错误
            /// </summary>
            [RemarkAttribute("验证码错误")]
            WrongVerify = 3,
            /// <summary>
            /// 账号被冻结
            /// </summary>
            [RemarkAttribute("账号被冻结")]
            Frozen = 4
        }

        /// <summary>
        /// 0成功 1用户不存在 2密码错误 3 验证码错误 4账号已冻结
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void UserLogout(this HttpContextBase context)
        {
            #region Cookie
            HttpCookie myCookie = context.Request.Cookies["CurrentUser"];
            if (myCookie != null)
            {
                myCookie.Expires = DateTime.Now.AddMinutes(-1);//设置过过期
                context.Response.Cookies.Add(myCookie);
            }

            #endregion Cookie

            #region Session
            var sessionUser = context.Session["CurrentUser"];
            if (sessionUser != null && sessionUser is CurrentUser)
            {
                CurrentUser currentUser = (CurrentUser)context.Session["CurrentUser"];
                logger.Debug(string.Format("用户id={0} Name={1}退出系统", currentUser.Id, currentUser.Name));
            }
            context.Session["CurrentUser"] = null;//表示将制定的键的值清空，并释放掉，
            context.Session.Remove("CurrentUser");
            context.Session.Clear();//表示将会话中所有的session的键值都清空，但是session还是依然存在，
            context.Session.RemoveAll();//
            context.Session.Abandon();//就是把当前Session对象删除了，下一次就是新的Session了   
            #endregion Session
        }
    }
}