using Hsf.MVC5.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace WebApi.Controllers
{


    public class UserManagerController : ApiController
    {
        [HttpGet]
        [Route("api/UserManager/DeleteUser")]
        public string DeleteUser(Guid? userGuid)
        {
            var result = new
            {
                Success = true,
                Message = "删除成功"
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        //[CustomAllowAnonymous]
        [HttpGet]
        [Route("api/UserManager/GetAjaxUserPageList")]
        public string GetAjaxUserPageList(int page, int limit, string keyWord, string starData, string endDate)
        {
            List<UserInfo> userlist = new List<UserInfo>();
            for (int i = 0; i < 300; i++)
            {
                userlist.Add(new UserInfo()
                {
                    UserGuid = Guid.NewGuid(),
                    UserName = $"用户名_{i}",
                    PhoneNum = (18671723698 + i).ToString(),
                    IsAdmin = false,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now.AddDays(i),
                    IsEnable = false
                });
            }

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                keyWord = keyWord.Trim();
                userlist = userlist.Where(u => u.UserName.Contains(keyWord) || u.PhoneNum.Contains(keyWord)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(starData))
            {
                var beginDate = Convert.ToDateTime(starData);
                userlist = userlist.Where(a => a.UpdateTime > beginDate).ToList();
            }

            if (!string.IsNullOrWhiteSpace(endDate))
            {
                var dtendDate = Convert.ToDateTime(starData);
                userlist = userlist.Where(a => a.UpdateTime < dtendDate).ToList();
            }

            var data = userlist.OrderByDescending(a => a.UpdateTime).Skip(limit * (page - 1)).Take(limit).ToList();
            var result = new
            {
                code = 0,
                msg = "",
                count = userlist.Count(),
                data = data
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }


        [HttpGet]
        [Route("api/UserManager/UserDetail")]
        public string UserDetail(Guid? userGuid)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new UserInfo()
            {
                UserGuid = Guid.NewGuid(),
                UserName = "Ricahrd",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                PhoneNum = "18672713698",
                IsAdmin = true
            });
        }
    }
}
