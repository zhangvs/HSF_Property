using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsf.Bussiness.Interface
{
    public interface Ititle_itemsService:IBaseService
    {
        void QueryWordsByTitleId(string tid);
    }
}
