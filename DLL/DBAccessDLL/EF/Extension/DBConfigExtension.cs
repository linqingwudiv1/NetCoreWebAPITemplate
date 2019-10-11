using DBAccessDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DBAccessDLL.EF
{
    /// <summary>
    /// 实体配置函数扩展
    /// </summary>
    public static class DBConfigExtension
    {
        /// <summary>
        /// 用于实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        static public EntityTypeBuilder<T> SetupBaseEntity<T>(this EntityTypeBuilder<T> targetBuilder, bool bUseSoftDelete = true) where T : BaseEntity
        {
            targetBuilder.Property<bool>     (  x => x.Qing_IsDelete    ).IsRequired(true).HasDefaultValue(false);
            targetBuilder.Property<Int64>    (  x => x.Qing_Version     ).IsRequired(true).HasDefaultValue(0);
            targetBuilder.Property<DateTime> (  x => x.Qing_CreateTime  ).IsRequired(true).HasDefaultValueSql("CURRENT_TIMESTAMP");
            targetBuilder.Property<DateTime> (  x => x.Qing_UpdateTime  ).IsRequired(true).HasDefaultValueSql("CURRENT_TIMESTAMP");
            targetBuilder.Property<Int64>    (  x => x.Qing_Sequence    ).IsRequired(true).HasDefaultValue(0);
            targetBuilder.Property<DateTime?>(  x => x.Qing_DeleteTime  ).IsRequired(false);

            if (bUseSoftDelete)
            {
                targetBuilder.UseSofeDelete();
            }
            
            return targetBuilder;
        }

        /// <summary>
        /// 用于Query 实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetBuilder"></param>
        /// <returns></returns>
        static public EntityTypeBuilder<T> UseSofeDelete<T>(this EntityTypeBuilder<T> targetBuilder) where T : BaseEntity 
        {
            targetBuilder.HasQueryFilter(x => x.Qing_IsDelete == false);
            return targetBuilder;
        }
    }
}
