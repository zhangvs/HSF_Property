using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hsf.MVC5.Models.User
{
    public class UserInfo
    {
        public Guid UserGuid { get; set; }

        public string UserName { get; set; }

        public string UserPassWord { get; set; }

        public string PhoneNum { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool? IsEnable { get; set; }
    }
}