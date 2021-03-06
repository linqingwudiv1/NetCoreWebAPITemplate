﻿using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RoutePages Get(long key)
        {
            return this.db.RoutePages.Find(key);
        }

        public IList<RoutePages> Get(IList<long> keys)
        {
            return this.db.RoutePages.Where(x => keys.Contains(x.Id)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        public int Add(RoutePages newEntiy)
        {
            this.db.RoutePages.Add(newEntiy);
            return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiys"></param>
        /// <returns></returns>
        public int Add(IList<RoutePages> newEntiys)
        {
            this.db.RoutePages.AddRange(newEntiys);
            return this.db.SaveChanges();
        }

        public int Delete(long key)
        {
            // routePage = this.Delete
            RoutePages routePage = this.db.RoutePages.Find(key);

            if (routePage != null)
            {
                var routePages      = ( from x in this.db.RoutePages     where x.Id == routePage.Id || EF.Functions.Like( x.HierarchyPath, $"{routePage.HierarchyPath}.%") select x );
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

        public int Update(RoutePages modifyEntiy)
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

        public int Update(IList<RoutePages> modifyEntiys)
        {
            throw new NotImplementedException();
        }
    }
}
