using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Security;

namespace Hsf.MVC5.Controllers
{
    public class LoginController : ApiController
    {

        [HttpGet]
        [Route("api/Login/Sign")]
        public string Sign(string userName, string passWord)
        {
            {
                // 编写用户登录的数据库验证逻辑
            }
            if ("Richard".Equals(userName) && "1".Equals(passWord))
            {
                FormsAuthenticationTicket ticketObj = new FormsAuthenticationTicket(0, userName, DateTime.Now, DateTime.Now.AddHours(1), true, $"{userName}&{passWord}", FormsAuthentication.FormsCookiePath);
                string ticket = FormsAuthentication.Encrypt(ticketObj);
                var result = new
                {
                    Result = true,
                    Message = "登录成功",
                    Ticket = ticket
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Result = false,
                    Message = "登录失败",
                    Ticket = string.Empty
                });
            }
        }
    }
}