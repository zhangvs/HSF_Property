using Hsf.Framework.Extension;
using Hsf.Framework.ImageHelper;
using Hsf.MVC5.Utility;
using Hsf.MVC5.Utility.Filter;
using Hsf.Web.Core.Filter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hsf.MVC5.Controllers
{
    public class FourthController : Controller
    {
        // GET: Fourth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [CustomActionFilter]
        [HttpGet]
        //[Route("api/Login/Sign")]
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

        [HttpPost]
        public ActionResult Login(string name, string password, string verify)
        {
            UserManage.LoginResult result = this.HttpContext.UserLogin(name, password, verify);

            if (result == UserManage.LoginResult.Success)
            {
                if (this.HttpContext.Session["CurrentUrl"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string url = this.HttpContext.Session["CurrentUrl"].ToString();
                    this.HttpContext.Session["CurrentUrl"] = null;
                    return Redirect(url);
                }
            }
            else
            {
                ModelState.AddModelError("failed", result.GetRemark());
                return View();
            }
        }
        /// <summary>
        /// 验证码 FileContentResult
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session["CheckCode"] = code;
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");//返回FileContentResult图片
        }
        /// <summary>
        /// 验证码  直接写入Response
        /// </summary>
        public void Verify()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session["CheckCode"] = code;
            bitmap.Save(base.Response.OutputStream, ImageFormat.Gif);
            base.Response.ContentType = "image/gif";
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            this.HttpContext.UserLogout();
            return RedirectToAction("Index", "Home"); ;
        }


        [CacheFilter(60)]
        [CompressFilter]
        [RenderFilter]
        public ViewResult TestFilter()
        {
            return View();
        }

        public ViewResult TestFilterUnCompress()
        {
            return View();
        }

        [MyActionFilterAttribute]
        public ViewResult TestMyFilter()
        {
            return View();
        }

        public ViewResult TestFilterException()
        {
            throw new Exception("TestFilterException Exception");
            return View();
        }
    }
}