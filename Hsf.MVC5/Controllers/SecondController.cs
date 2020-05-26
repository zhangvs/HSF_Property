using Hsf.Bussiness.Interface;
using Hsf.EF.Model;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace Hsf.MVC5.Controllers
{
    public class SecondController : Controller
    {
        private Ititle_itemsService _Ititle_itemsService = null;

        //替换了控制器的默认工厂，走UnityControllerFactoryNew工厂创建，构造函数创建，默认走参数最多的，通过配置文件自动注入参数
        public SecondController(Ititle_itemsService ititle_ItemsService)
        {
            this._Ititle_itemsService = ititle_ItemsService;
        }

        // GET: Second
        public ActionResult DB()
        {
            _Ititle_itemsService.QueryWordsByTitleId("7231958396958525839");
            return View();
        }
    }
}