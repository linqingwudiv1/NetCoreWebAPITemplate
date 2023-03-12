using BaseDLL;
using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

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
            targetBuilder.Property<bool>     ( x => x.IsDelete   ) .IsRequired( true  ) .HasDefaultValue(false);
            targetBuilder.Property<Int64>    ( x => x.Version    ) .IsRequired( true  ) .HasDefaultValue(0).IsConcurrencyToken(true);
            targetBuilder.Property<Int64>    ( x => x.Sequence   ) .IsRequired( true  ) .HasDefaultValue(0);
            targetBuilder.Property<DateTimeOffset> ( x => x.CreateTime ) .IsRequired( true  ) .HasDefaultValueSql("CURRENT_TIMESTAMP");
            targetBuilder.Property<DateTimeOffset> ( x => x.UpdateTime ) .IsRequired( true  ) .HasDefaultValueSql("CURRENT_TIMESTAMP");
            targetBuilder.Property<DateTimeOffset> ( x => x.DeleteTime ) .IsRequired( true  ) .HasDefaultValue<DateTimeOffset>(GVariable.DefDeleteTime);

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
            targetBuilder.HasQueryFilter( x => x.IsDelete == false );
            return targetBuilder;
        }

        /// <summary>
        /// convert table map to e.g:FullName becomes full_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetBuilder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static public EntityTypeBuilder<T> ToSnakeCaseTable<T>(this EntityTypeBuilder<T> targetBuilder) where T : class 
        {
            var result = Regex.Replace(typeof(T).Name, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]).ToLower();
            return targetBuilder.ToTable( result );
        }
    }
}
