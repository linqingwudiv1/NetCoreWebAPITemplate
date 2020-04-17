using BaseDLL;
using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Data.SqlTypes;

namespace DBAccessBaseDLL.EF.Entity
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
        /// <param name="targetBuilder"></param>
        /// <param name="bUseSoftDelete"></param>
        /// <returns></returns>
        static public EntityTypeBuilder<T> SetupBaseEntity<T>(this EntityTypeBuilder<T> targetBuilder, bool bUseSoftDelete = true) where T : BaseEntity
        {
            targetBuilder.Property<bool>     ( x => x.Q_IsDelete   ) .IsRequired( true  ) .HasDefaultValue(false);
            targetBuilder.Property<Int64>    ( x => x.Q_Version    ) .IsRequired( true  ) .HasDefaultValue(0).IsConcurrencyToken(true);
            targetBuilder.Property<Int64>    ( x => x.Q_Sequence   ) .IsRequired( true  ) .HasDefaultValue(0);
            targetBuilder.Property<DateTime> ( x => x.Q_CreateTime ) .IsRequired( true  ) .HasDefaultValueSql("CURRENT_TIMESTAMP");
            targetBuilder.Property<DateTime> ( x => x.Q_UpdateTime ) .IsRequired( true  ) .HasDefaultValueSql("CURRENT_TIMESTAMP");
            targetBuilder.Property<DateTime> ( x => x.Q_DeleteTime ) .IsRequired( true  ) .HasDefaultValue<DateTime>(GVariable.DefDeleteTime);

            if ( bUseSoftDelete )
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
            targetBuilder.HasQueryFilter( x => x.Q_IsDelete == false );
            return targetBuilder;
        }
    }
}
