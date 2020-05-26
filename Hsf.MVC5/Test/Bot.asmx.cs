using Hsf.Framework.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Hsf.MVC5.Test
{
    /// <summary>
    /// Bot 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Bot : System.Web.Services.WebService
    {
        [WebMethod]
        public string Sound(string msg)
        {
            string url = "http://openapi.tuling123.com/openapi/api/v2";
            var postData = "{\"reqType\":0,\"perception\": {\"inputText\": {\"text\": \"" + msg + "\"}},\"userInfo\": {\"apiKey\": \"0d3893875a164c0aaebad490b40c6fc9\",\"userId\": \"a1f31b0cd94e1e17\"}}";
            string dd = HttpUtil.PostUtil(url, postData);
            BotResponse botResponse = JsonConvert.DeserializeObject<BotResponse>(dd);
            int code = botResponse.intent.code;
            if (code != 5000)
            {
                return botResponse.results[0].values.text;
            }
            return null;
        }
    }

    public class Parameters
    {
        public string nearby_place { get; set; }
    }

    public class Intent
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string intentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string actionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Parameters parameters { get; set; }
    }

    public class Values
    {
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
    }

    public class ResultsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int groupType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resultType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Values values { get; set; }
    }

    public class BotResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Intent intent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ResultsItem> results { get; set; }
    }

}
