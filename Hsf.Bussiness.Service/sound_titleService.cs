using Hsf.EF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsf.Bussiness.Service
{
    public class sound_titleService : BaseService
    {
        public sound_titleService(DbContext context) : base(context)
        {

        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
