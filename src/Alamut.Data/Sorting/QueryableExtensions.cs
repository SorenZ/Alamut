using System;
using System.Linq;
using System.Linq.Dynamic;

namespace Alamut.Data.Sorting
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Sorts the specified query.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="query"> The query. </param>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, SortDescription[] sortDescriptions)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (sortDescriptions != null)
            {
                foreach (var sortDescription in sortDescriptions.Reverse())
                {
                    var property = sortDescription.PropertyName;
                    if (sortDescription.Direction == SortDirection.Descending)
                    {
                        property += " DESC";
                    }

                    query = query.OrderBy(property);
                }
            }

            return query;
        }
    }
}
