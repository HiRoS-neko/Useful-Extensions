using System.Collections.Generic;
using UnityEngine;

namespace UsefulExtensions.Components
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Retrieves all child transforms of the specified transform, optionally including recursive children.
        /// </summary>
        /// <param name="transform">The transform whose children are to be retrieved.</param>
        /// <param name="recurse">A boolean indicating whether to recursively include all descendants. If false, only immediate children are included.</param>
        /// <returns>An enumerable collection of child transforms.</returns>
        public static IEnumerable<Transform> Children(this Transform transform, bool recurse = true)
        {
            var childrenList = new List<Transform>();

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                childrenList.Add(child);
                if (recurse) childrenList.AddRange(child.Children());
            }

            return childrenList;
        }
    }
}
