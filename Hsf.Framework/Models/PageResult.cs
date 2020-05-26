using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsf.Framework.Models
{
    /// <summary>
    /// 每个层ul,bll.dll都可能用到的类，放到Framework里面model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T>
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> DataList { get; set; }
    }
}
