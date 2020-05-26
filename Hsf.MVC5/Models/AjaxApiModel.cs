using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hsf.MVC5.Models
{
    public class AjaxApiModel<T> where T : class
    {
        public AjaxApiModel()
        {
            Data = new List<T>();
        }
        public bool Result { get; set; } = false;

        public List<T> Data { get; set; }

        public string Message { get; set; }
    }
}