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
    public class hsf_ownerService: BaseService, Ihsf_ownerService
    {

        public hsf_ownerService(DbContext context) : base(context)
        {

        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
