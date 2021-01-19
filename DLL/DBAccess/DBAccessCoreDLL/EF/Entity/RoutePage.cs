using BaseDLL.Helper;
using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DBAccessCoreDLL.EF.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class RoutePage : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Nullable<Int64> ParentId { get; set; }

        /// <summary>
        /// 乘次路径,例如First.Second.Three.Four
        /// </summary>
        [Required]
        public string HierarchyPath { get; set; }

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
    public class RoutePageEFConfig : IEntityTypeConfiguration<RoutePage>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<RoutePage> builder)
        {
            var tableBuilder = builder.ToTable("RoutePage");
            /*
                   .OwnsOne<RoutePageMeta>(p => p.Meta, c =>
            {
                c.ToTable("RoutePageMeta");
            });
            */
            
            tableBuilder.Property(x => x.ParentId).HasDefaultValue(null);
#if DEBUG
            #region Default Database

            string path = Path.GetFullPath(PlatformServices.Default.Application.ApplicationBasePath + @"\.Config\routeDefaultData.json");
            var tree = JsonHelper.loadJsonFromFile<dynamic>(path);

            IList<RoutePage> defData = BuildTestData(tree.routes, null);

            tableBuilder.HasData(defData);
            #endregion
#endif
            tableBuilder.SetupBaseEntity<RoutePage>();
        }

#if DEBUG //写入测试数据

        static Int64 DefID = 10000;
        static Int64 MetaID = 1;
        private IList<RoutePage> BuildTestData(dynamic data, RoutePage parentNode)
        {
            
            List<RoutePage> test_data = new List<RoutePage>();
            foreach (dynamic item in data) 
            {
                long? parentID = parentNode == null ? default(long?) : parentNode.Id;
                long NewID = RoutePageEFConfig.DefID++;
                string HierarchyPath = parentNode == null ? "" : parentNode.HierarchyPath ;
                
                var routePage = new RoutePage()
                {
                    Id            = NewID,
                    ParentId      = parentID,
                    Component     = item.name          ?? ""      ,
                    RouteName     = item.name          ?? ""      ,
                    Path          = item.path          ?? ""      ,
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
