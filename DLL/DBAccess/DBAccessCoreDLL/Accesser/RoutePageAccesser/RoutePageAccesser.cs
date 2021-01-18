using DBAccessBaseDLL.Accesser;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
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

    public RoutePage Get(long key)
        {
            throw new NotImplementedException();
        }

        public IList<RoutePage> Get(IList<long> keys)
        {
            throw new NotImplementedException();
        }

        public int Add(RoutePage newEntiy)
        {
            throw new NotImplementedException();
        }

        public int Add(IList<RoutePage> newEntiys)
        {
            this.db.RoutePages.AddRange(newEntiys);
            return this.db.SaveChanges();
        }

        public int Delete(long key)
        {
            throw new NotImplementedException();
        }

        public int Delete(IList<long> keys)
        {
            throw new NotImplementedException();
        }

        public int Update(RoutePage modifyEntiy)
        {
            throw new NotImplementedException();
        }

        public int Update(IList<RoutePage> modifyEntiys)
        {
            throw new NotImplementedException();
        }
    }
}
