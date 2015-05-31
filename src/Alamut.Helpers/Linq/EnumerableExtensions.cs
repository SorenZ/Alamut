using System;
using System.Collections.Generic;
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
    }
}
