using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hsf.MVC5.Controllers
{
    public class FirstController : Controller
    {
        // GET: First
        public ActionResult Index()
        {
            return View();
        }

        public string String()
        {
            return "ceshi";
        }
        public string String2(string id)
        {
            return "id:"+id;
        }
        public string String3(string idid)
        {
            return "idid:" + idid;
        }

        public string Time(int year,int month,int day)
        {
            return $"{ year }-{ month }-{ day }";
        }
    }
}