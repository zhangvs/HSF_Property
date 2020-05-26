using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;

namespace Hsf.MVC5
{
    public class IOCConfig
    {
        public static IUnityContainer IOCContainer
        {
            private set;
            get;
        }

        //静态构造函数，类第一次加载的时候调用
        static IOCConfig()
        {
            InitIOCContainer();
        }

        public static void InitIOCContainer()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");//找配置文件的路径
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

            IUnityContainer container = new UnityContainer();
            section.Configure(container, "HsfContainer");
            IOCContainer = container;
        }
    }

    /// <summary>
    /// 替换掉默认工厂实例化，改为unity容器实例化
    /// </summary>
    public class UnityControllerFactoryNew : DefaultControllerFactory
    {
        private IUnityContainer UnityContainer
        {
            get
            {
                return IOCConfig.IOCContainer;
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
            IController controller = (IController)this.UnityContainer.Resolve(controllerType);
            return controller;
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            //释放对象
            //this.UnityContainer..Teardown(controller);//释放对象
        }
    }
}