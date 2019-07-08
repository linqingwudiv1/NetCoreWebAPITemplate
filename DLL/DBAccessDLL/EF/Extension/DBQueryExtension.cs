using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBAccessDLL.EF
{
    /// <summary>
    /// 
    /// </summary>
    static public class DBQueryExtension
    {
        /// <summary>
        /// 分页扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        static public IQueryable<T> QueryPages<T>(this IQueryable<T> query, int pageSize, int pageNum) where T : class
        {
            query.Skip(pageSize * (pageNum - 1)).Take(pageSize);
            return query;
        }

        /// <summary>
        /// 分页扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        static public IQueryable<T> QueryPages<T>(this DbSet<T> query, int pageSize, int pageNum) where T: class
        {
            query.Skip(pageSize * (pageNum - 1)).Take(pageSize);
            return query;
        }
    }
}
