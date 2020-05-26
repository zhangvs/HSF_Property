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
    public class title_itemsService : BaseService,Ititle_itemsService
    {
        public title_itemsService(DbContext context):base(context)
        {

        }

        public void QueryWordsByTitleId(string tid)
        {
            var model = from w in base.Set<baidu_items>()
                        join t in base.Set<sound_title>()
                        on w.titleid equals t.titleid
                        where t.titleid == tid
                        select w;//创建模型时无法使用上下文。如果上下文在OnModelCreating方法中使用，或者多个线程同时访问同一上下文实例，则可能引发此异常。注意，dbContext和相关类的实例成员不能保证是线程安全的。
            //The context cannot be used while the model is being created. This exception may be thrown if the context is used inside the OnModelCreating method or if the same context instance is accessed by multiple threads concurrently. Note that instance members of DbContext and related classes are not guaranteed to be thread safe.
            foreach (var item in model)
            {

            }

        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
