using BaseDLL.Helper;
using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.PlatformAbstractions;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace DBAccessCoreDLL.EFORM.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class RoutePages : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 Id { get; set; }

        public Nullable<Int64> ParentId { get; set; }

        /// <summary>
        /// 乘次路径,例如First.Second.Three.Four
        /// </summary>
        public string HierarchyPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Platform { get; set; }


        [Required]
        public string GroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string RouteName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string ActiveMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Affix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NoCache { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AlwaysShow { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hidden { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<RoutePageRole> RoutePageRoles { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RoutePageEFConfig : IEntityTypeConfiguration<RoutePages>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<RoutePages> builder)
        {
            var tableBuilder = builder.ToSnakeCaseTable();
            
            builder.Property(x => x.Platform).IsRequired(true).HasDefaultValue("");
            builder.Property(x => x.GroupName).IsRequired(true).HasDefaultValue("");
            tableBuilder.Property(x => x.ParentId).HasDefaultValue(null);
            tableBuilder.HasIndex(x => x.HierarchyPath).IsUnique(true);
#if DEBUG
            #region Default Database

            string path = Path.GetFullPath(PlatformServices.Default.Application.ApplicationBasePath + @"\.Config\routeDefaultData.json");
            var tree = JsonHelper.loadJsonFromFile<dynamic>(path);

            IList<RoutePages> defData = BuildTestData(tree.routes, null);

            tableBuilder.HasData(defData);
            #endregion
#endif
            tableBuilder.SetupBaseEntity<RoutePages>();
        }

#if DEBUG //写入测试数据

        static Int64 DefID = 10000;
        //static Int64 MetaID = 1;
        private IList<RoutePages> BuildTestData(dynamic data, RoutePages parentNode)
        {
            
            List<RoutePages> test_data = new List<RoutePages>();
            foreach (dynamic item in data) 
            {
                long? parentID = parentNode == null ? default(long?) : parentNode.Id;
                long NewID = RoutePageEFConfig.DefID++;
                string HierarchyPath = parentNode == null ? "" : parentNode.HierarchyPath ;
                
                var routePage = new RoutePages()
                {
                    Id            = NewID                             ,
                    ParentId      = parentID                          ,
                    Component     = item.component     ?? ""          ,
                    RouteName     = item.name,
                    Path          = item.path          ?? ""          ,
                    Redirect      = item.redirect      ?? null        ,
                    HierarchyPath = TreeHelper.GenerateHierarchyPath<long?>(HierarchyPath, NewID),
                    Title         = item.meta != null  ? item.meta.title            ?? ""    : ""    ,
                    Icon          = item.meta != null  ? item.meta.icon             ?? ""    : ""    ,
                    ActiveMenu    = item.meta != null  ? item.meta.activeMenu       ?? ""    : ""    ,
                    
                    Affix         = item.meta != null  ? item.meta.affix            ?? false : false ,
                    AlwaysShow    = item.meta != null  ? item.meta.alwaysShow       ?? false : false ,
                    NoCache       = item.meta != null  ? item.meta.noCache          ?? false : false ,
                    Hidden        = item.meta != null  ? item.meta.hidden           ?? false : false 
                };
                test_data.Add(routePage);

                if (item.children != null && item.children.Count > 0)
                {
                    var range = BuildTestData(item.children, routePage);
                    test_data.AddRange(range);
                }

            }

            return test_data;
        }
#endif


    }
}
