using Hsf.Bussiness.Interface;
using Hsf.EF.Model;
using Hsf.Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hsf.Bussiness.Service
{
    public class BaseService: IBaseService
    {
        #region Identity
        protected DbContext Context { get; private set; }
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="context"></param>
        public BaseService(DbContext context)
        {
            this.Context = context;
        }
        #endregion Identity

        public T Insert<T>(T t) where T : class
        {
            this.Context.Set<T>().Add(t);
            this.Commit();
            return t;
        }
        /// <summary>
        /// 添加一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns></returns>
        public IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class
        {
            this.Context.Set<T>().AddRange(tList);
            this.Commit();//一个链接  多个sql
            return tList;
        }

        #region Query
        public T Find<T>(string id) where T : class
        {
            return this.Context.Set<T>().Find(id);
        }

        /// <summary>
        /// 不应该暴露给上端使用者
        /// 整张表返回出去
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Set<T>() where T : class
        {
            return this.Context.Set<T>();
        }
        /// <summary>
        /// 这才是合理的做法，上端给条件，这里查询
        /// 表达式目录树，根据where条件返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return this.Context.Set<T>().Where<T>(funcWhere);
        }
        /// <summary>
        /// 分页查询,两次
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="funcWhere">查询条件（一个实体，满足什么条件）</param>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="funcOrderby">排序</param>
        /// <param name="isAsc">正序/倒序</param>
        /// <returns></returns>
        public PageResult<T> QueryPage<T, S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true) where T : class
        {
            var list = this.Set<T>();
            if (funcWhere != null)
            {
                list = list.Where<T>(funcWhere);
            }
            if (isAsc)
            {
                list = list.OrderBy(funcOrderby);
            }
            else
            {
                list = list.OrderByDescending(funcOrderby);
            }
            PageResult<T> result = new PageResult<T>()
            {
                DataList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = this.Context.Set<T>().Count(funcWhere)
            };
            return result;
        }
        #endregion

        /// <summary>
        /// 是没有实现查询，直接更新的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Update<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");

            this.Context.Set<T>().Attach(t);//将数据附加到上下文，支持实体修改和新实体，重置为UnChanged
            this.Context.Entry<T>(t).State = EntityState.Modified;
            this.Commit();//保存 然后重置为UnChanged
        }
        /*
         public enum EntityState
    {
        // 摘要:
        //     对象存在，但未由对象服务跟踪。在创建实体之后、但将其添加到对象上下文之前，该实体处于此状态。通过调用 System.Data.Objects.ObjectContext.Detach(System.Object)
        //     方法从上下文中移除实体后，或者使用 System.Data.Objects.MergeOption.NoTrackingSystem.Data.Objects.MergeOption
        //     加载实体后，该实体也会处于此状态。
        Detached = 1,
        //
        // 摘要:
        //     自对象加载到上下文中后，或自上次调用 System.Data.Objects.ObjectContext.SaveChanges() 方法后，此对象尚未经过修改。
        Unchanged = 2,
        //
        // 摘要:
        //     对象已添加到对象上下文，但尚未调用 System.Data.Objects.ObjectContext.SaveChanges() 方法。对象是通过调用
        //     System.Data.Objects.ObjectContext.AddObject(System.String,System.Object)
        //     方法添加到对象上下文中的。
        Added = 4,
        //
        // 摘要:
        //     使用 System.Data.Objects.ObjectContext.DeleteObject(System.Object) 方法从对象上下文中删除了对象。
        Deleted = 8,
        //
        // 摘要:
        //     对象已更改，但尚未调用 System.Data.Objects.ObjectContext.SaveChanges() 方法。
        Modified = 16,
    }
         */

        public void Update<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.Context.Set<T>().Attach(t);
                this.Context.Entry<T>(t).State = EntityState.Modified;
            }
            this.Commit();
        }

        /// <summary>
        /// 先附加 再删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Delete<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");
            this.Context.Set<T>().Attach(t);
            this.Context.Set<T>().Remove(t);
            this.Commit();
        }

        /// <summary>
        /// 还可以增加非即时commit版本的，
        /// 做成protected
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        public void Delete<T>(string Id) where T : class
        {
            T t = this.Find<T>(Id);//也可以附加
            if (t == null) throw new Exception("t is null");
            this.Context.Set<T>().Remove(t);
            this.Commit();
        }

        public void Delete<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.Context.Set<T>().Attach(t);
            }
            this.Context.Set<T>().RemoveRange(tList);
            this.Commit();
        }


        public void Commit()
        {
            this.Context.SaveChanges();
        }

        /// <summary>
        /// 可以直接执行查询sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class
        {
            return this.Context.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }
       /// <summary>
       /// 可以直接执行操作sql
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="sql"></param>
       /// <param name="parameters"></param>
        public void Excute<T>(string sql, SqlParameter[] parameters) where T : class
        {
            DbContextTransaction trans = null;
            try
            {
                trans = this.Context.Database.BeginTransaction();
                this.Context.Database.ExecuteSqlCommand(sql, parameters);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
        }

        public virtual void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }

    }
}
