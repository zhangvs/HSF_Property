using Hsf.EF.Model;
using Hsf.MVC5.Models;
using Hsf.MVC5.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace WebApi.Controllers
{
    public class SystemController : ApiController
    {
        [Route("api/System/GetMenuList")]
        //[CustomBasicAuthorize]
        public string GetMenuList()
        {
            AjaxApiModel<MenuInfo> result = new AjaxApiModel<MenuInfo>();
            List<MenuInfo> menus = new List<MenuInfo>() {
                new MenuInfo(){
                    MenuGuid=Guid.NewGuid(),
                    MenuName="菜单",
                    Ico="layui-icon-home",
                    Submenu=new List<MenuInfo>(){
                        new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="菜单1",
                            Ico="layui-icon-code-circle"
                        },
                         new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="菜单2",
                            Ico="layui-icon-engine"
                        }
                    }
                },

                new MenuInfo(){
                    MenuGuid=Guid.NewGuid(),
                    MenuName="系统",
                    Ico="layui-icon-component",
                    Submenu=new List<MenuInfo>(){
                        new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="用户管理",
                            Ico="layui-icon-username",
                            Url="Userview.html"
                        },
                        new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="菜单管理",
                            Ico="layui-icon-senior",
                            Url="MenuView.html"
                        },
                        new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="文件管理",
                            Ico="layui-icon-template",
                            Url="FileUpload.html"
                        },
                         new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="图片懒加载",
                            Ico="layui-icon-component",
                            Url="lazyImage.html"
                        },
                         new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="轮播",
                            Ico="layui-icon-component",
                            Url="WheelPlanting.html"
                        }
                    }
                }
            };
            result.Data = menus;
            result.Result = true;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result); ;
        }

        //[CustomBasicAuthorize]
        [Route("api/System/GetNavigationBarList")]
        public string GetNavigationBarList(int nav)
        {
            AjaxApiModel<MenuInfo> result = new AjaxApiModel<MenuInfo>();
            List<MenuInfo> menus = new List<MenuInfo>() {
                new MenuInfo(){
                    MenuGuid=Guid.NewGuid(),
                    MenuName="控制台",
                },
                new MenuInfo(){
                    MenuGuid=Guid.NewGuid(),
                    MenuName="商品管理",
                },
                 new MenuInfo(){
                    MenuGuid=Guid.NewGuid(),
                    MenuName="用户",
                },
                new MenuInfo(){
                    MenuGuid=Guid.NewGuid(),
                    MenuName="其他系统",
                    Ico="layui-icon-component",
                    Submenu=new List<MenuInfo>(){
                        new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="邮件管理"
                        },
                         new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="消息管理"
                        },
                         new MenuInfo(){
                            MenuGuid=Guid.NewGuid(),
                            MenuName="授权管理"
                        }
                    }
                }
            };

            result.Result = true;
            result.Data = menus;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result); ;
        }


        [HttpGet]
        [Route("api/System/GetTreeList")]
        //[CustomBasicAuthorize] 
        public string GetTreeList(string random)
        {
            AjaxApiModel<TreeResultData> result = new AjaxApiModel<TreeResultData>();
            List<TreeResultData> treeList = new List<TreeResultData>()
            {
                new TreeResultData(){
                    Id=new Guid("eed37c8f-50a7-4a3c-a894-67f159e05a58"),
                    Title="设置选中时候选中",
                    Href=null,
                    Disabled=false,
                    Spread=true,
                    Children=new List<TreeResultData>(){
                        new TreeResultData(){
                            Id=new Guid("d7623f12-d2fa-4ac5-a9c4-de3150dfc40b"),
                            Title="链接到百度",
                            Href="http://www.baidu.com",
                            Disabled=false
                        },
                        new TreeResultData(){
                            Id=new Guid("c5b685aa-555e-4dc2-96f5-2bb0134154be"),
                            Title="链接到360官网",
                            Href="https://www.360.cn/",
                            Disabled=false
                        }
                    }
                },
                new TreeResultData(){
                    Id=new Guid("c5b685aa-555e-4dc2-96f5-2bb0134154be"),
                    Title="节点2",
                    Href=null,
                    Disabled=false,
                    Children=new List<TreeResultData>(){
                        new TreeResultData(){
                            Id=new Guid("d6057540-3072-4afb-914c-40047baf0888"),
                            Title="当前节点不可选中",
                            Href="http://www.baidu.com",
                            Disabled=true,
                             Spread=true,
                        },
                        new TreeResultData(){
                            Id=new Guid("142319c8-b427-4d03-8546-146d4b76b5e9"),
                            Title="设置默认选中",
                            Href="http://www.baidu.com",
                            Checked=true,
                            Disabled=false,
                             Spread=true,
                        },
                         new TreeResultData(){
                            Id=new Guid("142319c8-b427-4d03-8546-146d4b76b5e1"),
                            Title="设置选中时候选中",
                            Href="http://www.baidu.com",
                            Checked=false,
                            Disabled=false,
                             Spread=true,
                        }
                    }
                }
            };
            result.Result = true;
            result.Data = treeList;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result); ;
        }

    }
}
