using DBAccessBaseDLL.EF.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace DBAccessBaseDLL.EF.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbContextExtension
    {


        /// <summary>
        /// 暖机扩展
        /// </summary>
        public static void WarmUp<T>(this T db)where T : BaseDBContext<T>
        {
            //var objectContext = ((IObjectContextAdapter)db).ObjectContext;
            //var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            //mappingCollection.GenerateViews(new List<EdmSchemaError>());
        }
    }
}
