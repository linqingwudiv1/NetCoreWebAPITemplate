using DBAccessBaseDLL.Accesser;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.EF.Context;
using Microsoft.EntityFrameworkCore.SqlServer;
using DBAccessCoreDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using EF_Alias = Microsoft.EntityFrameworkCore.EF;

namespace DBAccessCoreDLL.Accesser
{
    /// <summary>
    /// 
    /// </summary>
    public class RoutePageAccesser : IRoutePageAccesser
    {
        /// <summary>
        /// 
        /// </summary>
        CoreContextDIP IRoutePageAccesser.db { get => db; set => db = value ; }
        readonly IIDGenerator IDGenerator;
        private CoreContextDIP db;


        public RoutePageAccesser( IIDGenerator _IDGenerator, CoreContextDIP _db)
        {
            db = _db;
            IDGenerator = _IDGenerator;
        }

        public RoutePage Get(long key)
        {
            return this.db.RoutePages.Find(key);
        }

        public IList<RoutePage> Get(IList<long> keys)
        {
            return this.db.RoutePages.Where(x => keys.Contains(x.Id)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        public int Add(RoutePage newEntiy)
        {
            this.db.RoutePages.Add(newEntiy);
            return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiys"></param>
        /// <returns></returns>
        public int Add(IList<RoutePage> newEntiys)
        {
            this.db.RoutePages.AddRange(newEntiys);
            return this.db.SaveChanges();
        }

        public int Delete(long key)
        {
            // routePage = this.Delete
            RoutePage routePage = this.db.RoutePages.Find(key);

            if (routePage != null)
            {
                var routePages      = ( from x in this.db.RoutePages     where EF_Alias.Functions.Like( x.HierarchyPath, $"{routePage.HierarchyPath}%") select x );
                var routePageRoles  = ( from x in this.db.RoutePageRoles where routePages.Select(c => c.Id).Contains(x.RoutePageId) select x );

                this.db.RoutePageRoles.RemoveRange(routePageRoles);
                this.db.RoutePages.RemoveRange(routePages);
                return db.SaveChanges();
            }

            return 0;
        }

        public int Delete(IList<long> keys)
        {
            throw new NotImplementedException();
        }

        public int Update(RoutePage modifyEntiy)
        {
            var target = this.db.RoutePages.Find(modifyEntiy.Id);
            // modifyEntiy.ActiveMenu;
            if (target != null) 
            {
                //ensure  Hierarchy Level keep constant
                modifyEntiy.HierarchyPath = target.HierarchyPath;
                modifyEntiy.ParentId      = target.ParentId;
                
                this.db.Update(modifyEntiy);
            }
            
            return db.SaveChanges();
        }

        public int Update(IList<RoutePage> modifyEntiys)
        {
            throw new NotImplementedException();
        }
    }
}
