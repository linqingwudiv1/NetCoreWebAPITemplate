using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseDLL.Helper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeItem<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T item { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<TreeItem<T>> children { get; set; }
    }

    /// <summary>
    /// Example : var root = categories.GenerateTree(c => c.Id, c => c.ParentId);
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// 快速生成无序Tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id_selector">ID 属性</param>
        /// <param name="parent_id_selector">Parent ID 属性</param>
        /// <param name="root_id"></param>
        /// <returns></returns>
        public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(this IEnumerable<T> collection,
                                                       Func<T, K> id_selector,
                                                       Func<T, K> parent_id_selector,
                                                       K root_id = default(K))
        {
            foreach (var c in collection.Where(c => parent_id_selector(c).Equals(root_id)))
            {
                yield return new TreeItem<T>
                {
                    item = c,
                    children = collection.GenerateTree(id_selector, parent_id_selector, id_selector(c))
                };
            }
        }
    }
}
