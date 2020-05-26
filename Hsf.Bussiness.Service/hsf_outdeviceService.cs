using Hsf.Bussiness.Interface;
using Hsf.EF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsf.Bussiness.Service
{
    public class hsf_outdeviceService : BaseService, Ihsf_outdeviceService
    {

        public hsf_outdeviceService(DbContext context) : base(context)
        {

        }
        public override void Dispose()
        {
            base.Dispose();
        }

        public hsf_outdevice Create(hsf_outdevice hsf_Outdevice)
        {
            hsf_Outdevice.createtime = DateTime.Now;
            hsf_Outdevice.deletemark = 0;
            return hsf_Outdevice;
        }
    }
}
