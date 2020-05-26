using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hsf.MVC5.Models.System
{
    public class TreeResultData
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } // 树节点的 数据唯一编码

        [JsonProperty("title")]
        public string Title { get; set; } // 树结构的 文字描述

        [JsonProperty("href")]
        public string Href { get; set; }  // 超链接

        [JsonProperty("checked")]
        public bool Checked { get; set; }  // 设置默认选中

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }  //设置不可选择


        [JsonProperty("spread")]
        public bool Spread { get; set; }  //设置默认展开

        [JsonProperty("children")]
        public List<TreeResultData> Children { get; set; } // 递归
    }
}