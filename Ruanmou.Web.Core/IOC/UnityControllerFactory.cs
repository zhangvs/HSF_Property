﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ruanmou.Web.Core.IOC
{
    /// <summary>
    /// 自定义的控制器实例化工厂
    /// </summary>
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private IUnityContainer UnityContainer
        {
            get
            {
                return DIFactory.GetContainer();
            }
        }

      

        /// <summary>
        /// 创建控制器对象
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (null == controllerType)
            {
                return null;
            }
            IController controller= (IController)this.UnityContainer.Resolve(controllerType);
            return controller;
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            this.UnityContainer.Teardown(controller);//释放对象
        }
    }
}
