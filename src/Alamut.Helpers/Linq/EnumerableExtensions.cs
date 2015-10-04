using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alamut.Helpers.Linq
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// determine whether the collection is not null AND has values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <remarks>
        /// based on : http://stackoverflow.com/a/5047373/428061
        /// </remarks>
        public static bool IsAny<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }

        /// <summary>
        /// provide dynamic projection by list of fields
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="input"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IEnumerable<object> DynamicSelect<TSource>(this IEnumerable<TSource> source, 
            object input,
            IEnumerable<string> fields)
        {
            return source.Select(s => DynamicProjection(input, fields));
        }


        static object DynamicProjection(object input, IEnumerable<string> properties)
        {
            var type = input.GetType();
            dynamic dObject = new ExpandoObject();
            var dDict = dObject as IDictionary<string, object>;

            foreach (var p in properties)
            {
                var field = type.GetField(p);
                if (field != null)
                    dDict[p] = field.GetValue(input);

                var prop = type.GetProperty(p);
                if (prop != null && prop.GetIndexParameters().Length == 0)
                    dDict[p] = prop.GetValue(input, null);
            }

            return dObject;
        }
    }
}
