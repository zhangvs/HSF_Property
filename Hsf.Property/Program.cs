using Hsf.Bussiness.Interface;
using Hsf.Bussiness.Service;
using Hsf.EF.Model;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using System.Configuration;
using System.IO;

namespace Hsf.Property
{
    class Program
    {
        static void Main(string[] args)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");//找配置文件的路径
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

            IUnityContainer container = new UnityContainer();
            section.Configure(container, "HsfContainer");

            using (Ititle_itemsService service = container.Resolve<Ititle_itemsService>())
            {
                //var dd = hsf_OwnerService.Find<hsf_owner>("5c9a66ee-cb9b-4d70-8b67-592a425a5019");
                service.QueryWordsByTitleId("7231958396958525839");
            }
        }
    }
}
