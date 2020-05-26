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

namespace Hsf.MVC5.Areas.System.Controllers
{
    public class OwnerController : Controller
    {
        private Logger logger = Logger.CreateLogger(typeof(OwnerController));
        private Ihsf_ownerService ihsf_ownerService = null;//1 提供数据访问服务
        private int pageSize = 5;

        /// <summary>
        /// 要配置文件中添加
        /// </summary>
        /// <param name="hsf_ownerService"></param>
        public OwnerController(Ihsf_ownerService hsf_ownerService)
        {
            this.ihsf_ownerService = hsf_ownerService;
        }


        // GET: Third
        [AllowAnonymous]
        public ActionResult Index(string searchString, int orderBy = -1, int pageIndex = 1)
        {
            logger.Info("Third/Index");
            ViewBag.SearchString = searchString;


            //string orderBy = Request.QueryString["orderBy"];
            //string searchString = Request.QueryString["searchString"];

            Expression<Func<hsf_owner, bool>> lambdaWhere = t => t.deletemark == 0;


            if (!string.IsNullOrWhiteSpace(searchString))
            {
                lambdaWhere = lambdaWhere.And<hsf_owner>(b => b.telphone.Contains(searchString));
            }

            #region order
            bool isAsc = true;
            Expression<Func<hsf_owner, string>> lambdaOrderby = null;
            if (orderBy == -1)
            {
                ViewBag.OrderBy = 1;
                isAsc = false;
                lambdaOrderby = t => t.createtime.ToString();
                //默认按聚集索引排序
            }
            else if (orderBy == 0)
            {
                ViewBag.OrderBy = 1;
                lambdaOrderby = t => t.host.ToString() ?? "";
            }
            else
            {
                ViewBag.OrderBy = 0;
                isAsc = false;
                lambdaOrderby = t => t.host.ToString() ?? "";
            }

            #endregion order
            //var hsf_ownerList = lambdaWhere == null ? ihsf_ownerService.Query(lambdaWhere) : ihsf_ownerService.Set<hsf_owner>();
            //if (isAsc)
            //    hsf_ownerList = hsf_ownerList.OrderBy(lambdaOrderby);
            //else
            //    hsf_ownerList = hsf_ownerList.OrderByDescending(lambdaOrderby);
            //return View(hsf_ownerList.ToPagedList<hsf_owner>(Math.Max(1, pageIndex), pageSize));

            PageResult<hsf_owner> hsf_ownerList = this.ihsf_ownerService.QueryPage(lambdaWhere, pageSize, pageIndex, lambdaOrderby, isAsc);
            StaticPagedList<hsf_owner> pageList = new StaticPagedList<hsf_owner>(hsf_ownerList.DataList, pageIndex, pageSize, hsf_ownerList.TotalCount);
            return View(pageList);
        }


        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.categoryList = BuildCategoryList();
            return View();
        }

        /// <summary>
        /// Action前加[HttpPost]表示只有以Post方法请求Create Action的时候才会调用这个Action。
        ///
        ///Action的参数是以hsf_owner实例传递的。也就是说Create.cshtml提交的4个值被赋值给work然后把worker传递给Create作为参数。而这个参数前面的[Bind(Include = ProductId, CategoryId, Title, Price, Url, ImageUrl")]是为了防止过多提交(overposting)攻击的。从Create.cshtml的代码可以知道，这个页面只会提交4个值。而黑客可以有办法通过这个页面提/交更多的值给/当/前Action，而这些多出来的值也会存在worker实例中被添加到数据库，这无疑是危险的。因此[Bind(Include = "")]就限定了不管你提交多少值，我这个Action里只/接受ProductId, CategoryId, Title, Price, Url, ImageUrl。保证了页面的安全性。
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
        public ActionResult Create([Bind(Include = "Id, telphone, password, chinaname, building, unit, floor, room")]hsf_owner hsf_owner)
        {
            string title1 = this.HttpContext.Request.Params["title"];
            string title2 = this.HttpContext.Request.QueryString["title"];
            string title3 = this.HttpContext.Request.Form["title"];
            if (ModelState.IsValid)//
            {
                hsf_owner newhsf_owner = ihsf_ownerService.Insert(hsf_owner);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("ModelState未通过检测");
            }
        }

        [HttpPost]
        public ActionResult AjaxCreate([Bind(Include = "Id, telphone, password, chinaname, building, unit, floor, room")]hsf_owner hsf_owner)
        {
            if (!string.IsNullOrEmpty(hsf_owner.chinaname))
            {
                hsf_owner.residential = PingYinHelper.GetFirstSpell(hsf_owner.chinaname);//生成小区首字母
                hsf_owner.Id = Guid.NewGuid().ToString();
                hsf_owner.deletemark = 0;
                hsf_owner.host = hsf_owner.residential + "-" + hsf_owner.building + "-" + hsf_owner.unit + "-" + hsf_owner.floor + "-" + hsf_owner.room;
                hsf_owner.createtime = DateTime.Now;
                hsf_owner newhsf_owner = ihsf_ownerService.Insert(hsf_owner);
                AjaxResult ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Success,
                    PromptMsg = "插入成功"
                };
                return Json(ajaxResult);
            }
            else
            {
                logger.Debug($"error:小区不能为空！");
                AjaxResult ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Failed,
                    PromptMsg = "小区不能为空"
                };
                return Json(ajaxResult);
            }

        }
        #endregion Create

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
                throw new Exception("need hsf_owner id");
            }
            hsf_owner hsf_owner = ihsf_ownerService.Find<hsf_owner>(id);
            if (hsf_owner == null)
            {
                throw new Exception("Not Found hsf_owner");
            }
            return View(hsf_owner);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                throw new Exception("need hsf_owner id");
            }
            hsf_owner hsf_owner = ihsf_ownerService.Find<hsf_owner>(id);// ?? -1
            if (hsf_owner == null)
            {
                throw new Exception("Not Found");
            }
            //ViewBag.categoryList = BuildCategoryList();
            return View(hsf_owner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, telphone, password, chinaname, building, unit, floor, room")] hsf_owner hsf_owner)
        {
            if (ModelState.IsValid)
            {
                hsf_owner hsf_ownerDB = ihsf_ownerService.Find<hsf_owner>(hsf_owner.Id);
                hsf_ownerDB.telphone = hsf_owner.telphone;
                hsf_ownerDB.password = hsf_owner.password;
                hsf_ownerDB.chinaname = hsf_owner.chinaname;
                hsf_ownerDB.building = hsf_owner.building;
                hsf_ownerDB.unit = hsf_owner.unit;
                hsf_ownerDB.floor = hsf_owner.floor;
                hsf_ownerDB.room = hsf_owner.room;
                hsf_ownerDB.residential = PingYinHelper.GetFirstSpell(hsf_ownerDB.chinaname);//生成小区首字母
                hsf_ownerDB.host = hsf_ownerDB.residential + "-" + hsf_ownerDB.building + "-" + hsf_ownerDB.unit + "-" + hsf_ownerDB.floor + "-" + hsf_ownerDB.room;
                hsf_ownerDB.createtime = DateTime.Now;
                ihsf_ownerService.Update(hsf_ownerDB);
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
            hsf_owner hsf_owner = ihsf_ownerService.Find<hsf_owner>(id);
            if (hsf_owner == null)
            {
                throw new Exception("Not Found");
            }
            else
            {
                ihsf_ownerService.Delete(hsf_owner);
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
            ihsf_ownerService.Delete<hsf_owner>(id);
            AjaxResult ajaxResult = new AjaxResult()
            {
                Result = DoResult.Success,
                PromptMsg = "删除成功"
            };
            return Json(ajaxResult);
        }
        #endregion Details Edit Delete



        #region lucene搜索
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="firstCategory"></param>
        /// <param name="secondCategory"></param>
        /// <param name="thirdCategory"></param>
        /// <returns></returns>
        public ActionResult SearchIndex()
        {
            return View();
        }

        /// <summary>
        /// 列表页：局部页的方式
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageIndex"></param>
        /// <param name="firstCategory"></param>
        /// <param name="secondCategory"></param>
        /// <param name="thirdCategory"></param>
        /// <returns></returns>
        public ActionResult SearchPartialList(string searchString, int orderBy = -1, int pageIndex = 1)
        {
            logger.Info("Third/Index");
            ViewBag.SearchString = searchString;


            //string orderBy = Request.QueryString["orderBy"];
            //string searchString = Request.QueryString["searchString"];

            Expression<Func<hsf_owner, bool>> lambdaWhere = t => t.deletemark == 0;


            if (!string.IsNullOrWhiteSpace(searchString))
            {
                lambdaWhere = lambdaWhere.And<hsf_owner>(b => b.telphone.Contains(searchString));
            }

            #region order
            bool isAsc = true;
            Expression<Func<hsf_owner, string>> lambdaOrderby = null;
            if (orderBy == -1)
            {
                ViewBag.OrderBy = 1;
                isAsc = false;
                lambdaOrderby = t => t.createtime.ToString();
                //默认按聚集索引排序
            }
            else if (orderBy == 0)
            {
                ViewBag.OrderBy = 1;
                lambdaOrderby = t => t.host.ToString() ?? "";
            }
            else
            {
                ViewBag.OrderBy = 0;
                isAsc = false;
                lambdaOrderby = t => t.host.ToString() ?? "";
            }

            #endregion order
            //var hsf_ownerList = lambdaWhere == null ? ihsf_ownerService.Query(lambdaWhere) : ihsf_ownerService.Set<hsf_owner>();
            //if (isAsc)
            //    hsf_ownerList = hsf_ownerList.OrderBy(lambdaOrderby);
            //else
            //    hsf_ownerList = hsf_ownerList.OrderByDescending(lambdaOrderby);
            //return View(hsf_ownerList.ToPagedList<hsf_owner>(Math.Max(1, pageIndex), pageSize));

            PageResult<hsf_owner> hsf_ownerList = this.ihsf_ownerService.QueryPage(lambdaWhere, pageSize, pageIndex, lambdaOrderby, isAsc);
            StaticPagedList<hsf_owner> pageList = new StaticPagedList<hsf_owner>(hsf_ownerList.DataList, pageIndex, pageSize, hsf_ownerList.TotalCount);
            return View(pageList);
        }
        #endregion lucene搜索
    }
}