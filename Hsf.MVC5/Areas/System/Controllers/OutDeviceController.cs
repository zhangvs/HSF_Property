using Hsf.Bussiness.Interface;
using Hsf.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using Hsf.Framework.Log;
using Hsf.Framework.ExtendExpression;
using Hsf.Framework.Models;
using PagedList;//2 PagedList
using Hsf.Web.Core.Models;
using Hsf.Framework.Helper;
using Hsf.Web.Core.Filter;
using Hsf.Redis.Service;

namespace Hsf.MVC5.Areas.System.Controllers
{
    public class OutDeviceController : Controller
    {
        /// </summary>
        #region Identity
        private Ihsf_outdeviceService ihsf_outdeviceService = null;//1 提供数据访问服务
        private int pageSize = 5;
        private Logger logger = Logger.CreateLogger(typeof(OutDeviceController));

        /// <summary>
        /// 要配置文件中添加
        /// </summary>
        /// <param name="hsf_outdeviceService"></param>
        public OutDeviceController(Ihsf_outdeviceService hsf_outdeviceService)
        {
            this.ihsf_outdeviceService = hsf_outdeviceService;
        }
        #endregion Identity

        // GET: Third
        [AllowAnonymous]
        public ActionResult Index(string searchString, int orderBy = -1, int pageIndex = 1)
        {
            ViewBag.SearchString = searchString;


            //string orderBy = Request.QueryString["orderBy"];
            //string searchString = Request.QueryString["searchString"];

            Expression<Func<hsf_outdevice, bool>> lambdaWhere = t => t.deletemark == 0;


            if (!string.IsNullOrWhiteSpace(searchString))
            {
                lambdaWhere = lambdaWhere.And<hsf_outdevice>(b => b.deviceid.Contains(searchString));
            }

            #region order
            bool isAsc = true;
            Expression<Func<hsf_outdevice, string>> lambdaOrderby = null;
            if (orderBy == -1)
            {
                ViewBag.OrderBy = 1;
                isAsc = false;
                lambdaOrderby = t => t.createtime.ToString();
                //默认按聚集索引排序
            }

            #endregion order
            //var hsf_outdeviceList = lambdaWhere == null ? ihsf_outdeviceService.Query(lambdaWhere) : ihsf_outdeviceService.Set<hsf_outdevice>();
            //if (isAsc)
            //    hsf_outdeviceList = hsf_outdeviceList.OrderBy(lambdaOrderby);
            //else
            //    hsf_outdeviceList = hsf_outdeviceList.OrderByDescending(lambdaOrderby);
            //return View(hsf_outdeviceList.ToPagedList<hsf_outdevice>(Math.Max(1, pageIndex), pageSize));

            PageResult<hsf_outdevice> hsf_outdeviceList = this.ihsf_outdeviceService.QueryPage(lambdaWhere, pageSize, pageIndex, lambdaOrderby, isAsc);
            StaticPagedList<hsf_outdevice> pageList = new StaticPagedList<hsf_outdevice>(hsf_outdeviceList.DataList, pageIndex, pageSize, hsf_outdeviceList.TotalCount);
            return View(pageList);
        }


        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.categoryList = BuildCategoryList();
            return View();
        }



        /// <summary>
        /// Action前加[HttpPost]表示只有以Post方法请求Create Action的时候才会调用这个Action。
        ///
        ///Action的参数是以hsf_outdevice实例传递的。也就是说Create.cshtml提交的4个值被赋值给work然后把worker传递给Create作为参数。而这个参数前面的[Bind(Include = ProductId, CategoryId, Title, Price, Url, ImageUrl")]是为了防止过多提交(overposting)攻击的。从Create.cshtml的代码可以知道，这个页面只会提交4个值。而黑客可以有办法通过这个页面提/交更多的值给/当/前Action，而这些多出来的值也会存在worker实例中被添加到数据库，这无疑是危险的。因此[Bind(Include = "")]就限定了不管你提交多少值，我这个Action里只/接受ProductId, CategoryId, Title, Price, Url, ImageUrl。保证了页面的安全性。
        ///第7行ModelState.IsValid表示提交的数据是否有效。比如对于一个类型为数字的属性必须提交一个数字才算是有效。如果提交的数据有效则保存数据并且将页面跳转回Index.cshtml。
        ///第16行ModelState.AddModelError()函数可以给Model添加一条错误信息，函数的第一个参数是key，用于查找这个错误信息，第二个参数是错误信息的具体内容。这个错误信息可以在View中通过Html.ValidationMessage("unableToSave")来访问到。
        ///
        /// 
        /// 页面上的Html.AntiForgeryToken()会给访问者一个默认名为__RequestVerificationToken的cookie
        /// 为了验证一个来自form post，还需要在目标action上增加[ValidateAntiForgeryToken]特性，它是一个验证过滤器，
        /// 它主要检查
        /// 
        /// (1)请求的是否包含一个约定的AntiForgery名的cookie
        /// 
        /// (2)请求是否有一个Request.Form["约定的AntiForgery名"]，约定的AntiForgery名的cookie和Request.Form值是否匹配
        /// </summary>
        /// <param name="worker"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, residential, building, unit, deviceid, chinaname, devtype")]hsf_outdevice hsf_outdevice)
        {
            if (ModelState.IsValid)
            {
                hsf_outdevice.createtime = DateTime.Now;
                hsf_outdevice.deletemark = 0;
                hsf_outdevice newhsf_outdevice = ihsf_outdeviceService.Insert(hsf_outdevice);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("ModelState未通过检测");
            }
        }


        [HttpPost]
        public ActionResult AjaxCreate([Bind(Include = "Id, residential, building, unit, deviceid, chinaname, devtype")]hsf_outdevice hsf_outdevice)
        {
            if (!string.IsNullOrEmpty(hsf_outdevice.deviceid))
            {
                hsf_outdevice.Id = Guid.NewGuid().ToString();
                hsf_outdevice.createtime = DateTime.Now;
                hsf_outdevice.deletemark = 0;
                hsf_outdevice newhsf_outdevice = ihsf_outdeviceService.Insert(hsf_outdevice);

                using (RedisHashService service = new RedisHashService())
                {
                    //室外，大华产品
                    string _residential = hsf_outdevice.residential;//小区
                    string _building = hsf_outdevice.building;//楼号
                    string _unit = hsf_outdevice.unit;//单元
                    string cachekey = _residential + "-" + _building + "-" + _unit;//默认房间

                    //清除当前设备类型的设备列表缓存
                    if (string.IsNullOrEmpty(_building) && string.IsNullOrEmpty(_unit))
                    {
                        //MMSJ--,整个所在小区添加公共设备，比如公共摄像头，所有业主的缓存全部删除
                        List<string> ods = service.GetHashKeys("OutDevices");
                        foreach (var item in ods)
                        {
                            if (item.Contains(_residential))
                            {
                                service.RemoveEntryFromHash("OutDevices", item);
                            }
                        }
                    }
                    else if(string.IsNullOrEmpty(_unit))
                    {
                        //MMSJ-1-所在楼号添加公共设备，比如楼号摄像头，楼号内业主的缓存全部删除
                        List<string> ods = service.GetHashKeys("OutDevices");
                        foreach (var item in ods)
                        {
                            if (item.Contains(_residential + "-" + _building))
                            {
                                service.RemoveEntryFromHash("OutDevices", item);
                            }
                        }
                    }
                    else
                    {
                        //MMSJ-1-1所在单元设备变动，比如单元门，单元内业主的缓存全部删除
                        service.RemoveEntryFromHash("OutDevices", cachekey);
                    }
                    
                    logger.Debug($"清除当前单元的室外设备列表 {cachekey}");
                }

                AjaxResult ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Success,
                    PromptMsg = "插入成功"
                };
                return Json(ajaxResult);
            }
            else
            {
                logger.Debug($"error:id不能为空！");
                AjaxResult ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Failed,
                    PromptMsg = "id不能为空"
                };
                return Json(ajaxResult);
            }
        }



        #region Details Edit Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                throw new Exception("need hsf_outdevice id");
            }
            hsf_outdevice hsf_outdevice = ihsf_outdeviceService.Find<hsf_outdevice>(id);
            if (hsf_outdevice == null)
            {
                throw new Exception("Not Found hsf_outdevice");
            }
            return View(hsf_outdevice);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                throw new Exception("need hsf_outdevice id");
            }
            hsf_outdevice hsf_outdevice = ihsf_outdeviceService.Find<hsf_outdevice>(id);// ?? -1
            if (hsf_outdevice == null)
            {
                throw new Exception("Not Found");
            }
            //ViewBag.categoryList = BuildCategoryList();
            return View(hsf_outdevice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "residential, building, unit, deviceid, chinaname, devtype")] hsf_outdevice hsf_outdevice)
        {
            if (ModelState.IsValid)
            {
                hsf_outdevice hsf_outdeviceDB = ihsf_outdeviceService.Find<hsf_outdevice>(hsf_outdevice.Id);
                hsf_outdeviceDB.residential = hsf_outdevice.residential;
                hsf_outdeviceDB.building = hsf_outdevice.building;
                hsf_outdeviceDB.unit = hsf_outdevice.unit;
                hsf_outdeviceDB.deviceid = hsf_outdevice.deviceid;
                hsf_outdeviceDB.chinaname = hsf_outdevice.chinaname;
                hsf_outdeviceDB.devtype = hsf_outdevice.devtype;
                hsf_outdeviceDB.createtime = DateTime.Now;
                ihsf_outdeviceService.Update(hsf_outdeviceDB);

                using (RedisHashService service = new RedisHashService())
                {
                    //室外，大华产品
                    string _residential = hsf_outdevice.residential;//小区
                    string _building = hsf_outdevice.building;//楼号
                    string _unit = hsf_outdevice.unit;//单元
                    string cachekey = _residential + "-" + _building + "-" + _unit;//默认房间

                    //清除当前设备类型的设备列表缓存
                    service.RemoveEntryFromHash("OutDevices", cachekey);
                    logger.Debug($"清除当前单元的室外设备列表 {cachekey}");
                }

                return RedirectToAction("Index");
            }
            else
                throw new Exception("ModelState未通过检测");
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                throw new Exception("Not Found");
            }
            hsf_outdevice hsf_outdevice = ihsf_outdeviceService.Find<hsf_outdevice>(id);
            if (hsf_outdevice == null)
            {
                throw new Exception("Not Found");
            }
            else
            {
                ihsf_outdeviceService.Delete(hsf_outdevice);

                using (RedisHashService service = new RedisHashService())
                {
                    //室外，大华产品
                    string _residential = hsf_outdevice.residential;//小区
                    string _building = hsf_outdevice.building;//楼号
                    string _unit = hsf_outdevice.unit;//单元
                    string cachekey = _residential + "-" + _building + "-" + _unit;//默认房间

                    //清除当前设备类型的设备列表缓存
                    service.RemoveEntryFromHash("OutDevices", cachekey);
                    logger.Debug($"清除当前单元的室外设备列表 {cachekey}");
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// ajax删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxDelete(string id)
        {
            if (id == null)
            {
                throw new Exception("Not Found");
            }
            hsf_outdevice hsf_outdevice = ihsf_outdeviceService.Find<hsf_outdevice>(id);
            if (hsf_outdevice == null)
            {
                throw new Exception("Not Found");
            }
            else
            {
                ihsf_outdeviceService.Delete<hsf_outdevice>(id);

                using (RedisHashService service = new RedisHashService())
                {
                    //室外，大华产品
                    string _residential = hsf_outdevice.residential;//小区
                    string _building = hsf_outdevice.building;//楼号
                    string _unit = hsf_outdevice.unit;//单元
                    string cachekey = _residential + "-" + _building + "-" + _unit;//默认房间

                    //清除当前设备类型的设备列表缓存
                    service.RemoveEntryFromHash("OutDevices", cachekey);
                    logger.Debug($"清除当前单元的室外设备列表 {cachekey}");
                }

                AjaxResult ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Success,
                    PromptMsg = "删除成功"
                };
                return Json(ajaxResult);
            }

        }
        #endregion Details Edit Delete
    }
}