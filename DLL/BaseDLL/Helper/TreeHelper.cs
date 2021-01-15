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
        public T node { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<TreeItem<T>> children { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int deep {get;set;}
    }

    /// <summary>
    /// Example : var root = categories.GenerateTree(c => c.Id, c => c.ParentId);
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// 快速生成无序Tree 
        /// Example : var root = categories.GenerateTree(c => c.Id, c => c.ParentId);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id_selector">ID 属性</param>
        /// <param name="parent_id_selector">Parent ID 属性</param>
        /// <param name="root_id"></param>
        /// /// <param name="root_deep"></param>
        /// <returns></returns>
        public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(this IEnumerable<T> collection,
                                                       Func<T, K> id_selector,
                                                       Func<T, K> parent_id_selector,
                                                       K root_id = default(K),
                                                       int root_deep = 0)
        {
            foreach (T c in collection.Where(c => parent_id_selector(c).Equals(root_id)))
            {
                yield return new TreeItem<T>
                {
                    node = c,
                    children = collection.GenerateTree(id_selector, parent_id_selector, id_selector(c) , root_deep: root_deep + 1),
                    deep = root_deep
                };
            }
        }


        /// <summary>
        /// 生成无序Tree,指定的数据结构填充
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id_selector"></param>
        /// <param name="children_evaluator">赋值器  
        /// demo : 
        ///     (object c, IEnumerable<object> val) =>
        ///     {
        ///         c.children = val.ToArray();
        ///     }
        /// </param>
        /// <param name="root_id"></param>
        /// <returns></returns>
        public static IEnumerable<T> GenerateTree<T, K>(this IEnumerable<T> collection,
                                                       Func<T, K> id_selector,
                                                       Func<T, K> parent_id_selector,
                                                       Action<T, IEnumerable<T>> children_evaluator,
                                                       K root_id = default(K) )
        {
            foreach (T c in collection.Where(c => parent_id_selector(c).Equals(root_id))) 
            {
                IEnumerable<T> children = collection.GenerateTree(id_selector, parent_id_selector, children_evaluator, id_selector(c));
                children_evaluator(c, children);
                yield return c;
            }
        }


        /// <summary>
        /// 遍历 Tree
        /// </summary>
        /// <param name="collection"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static void Foreach<T>( this IEnumerable<T> collection, 
                                       Func<T, IEnumerable<T>> children_selector,
                                       Action<T> predicate )
        {
            foreach (T node in collection )
            {
                predicate(node);
                IEnumerable<T> children = children_selector(node);
                children.Foreach(children_selector, predicate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="parentHierarchyPath"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GenerateHierarchyPath<K>(string parentHierarchyPath, K key)
        {
            string keyid = (key == null ? "" : key.ToString().Trim());
            if (string.IsNullOrEmpty(parentHierarchyPath))
            {
                return keyid;
            }
            else
            {
                return $"{parentHierarchyPath}.{keyid}";
            }
        }


    }
}
