using System;
using System.Linq;

using System.Linq.Dynamic;

namespace Alamut.Data.Linq
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

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="query"> The query. </param>
        /// <param name="startIndex"> Start index of the row. </param>
        /// <param name="itemCount"> Size of the page. </param>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, int startIndex, int itemCount, SortDescription[] sortDescriptions)
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

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            return query.Skip(startIndex).Take(itemCount);
        }

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="paginatedCriteria">The paginated criteria.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">query</exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, IPaginatedCriteria paginatedCriteria)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.ToPage(paginatedCriteria.StartIndex, paginatedCriteria.PageSize, paginatedCriteria.SortDescriptions);
        }

        /// <summary>
        /// Creates an <see cref="IPaginated{T}" /> instance from the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="paginatedCriteria">The paginated criteria.</param>
        /// <returns></returns>
        public static IPaginated<T> ToPaginated<T>(this IQueryable<T> query, IPaginatedCriteria paginatedCriteria)
        {
            return new Paginated<T>(
                query.ToPage(paginatedCriteria.StartIndex, paginatedCriteria.PageSize, paginatedCriteria.SortDescriptions),
                query.Count(),
                paginatedCriteria.CurrentPage,
                paginatedCriteria.PageSize);
        }

    }
}
