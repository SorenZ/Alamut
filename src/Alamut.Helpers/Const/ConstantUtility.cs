using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alamut.Helpers.Const
{
    public static class ConstantUtility
    {
        /// <summary>
        /// provides a constants of on object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>based on this article : http://stackoverflow.com/a/10261848/428061 </remarks>
        public static IEnumerable<FieldInfo> GetConstants(this IReflect type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }
    }
}
