using System.Collections.Generic;

namespace Alamut.Data.Paging
{
    /// <summary>
    /// Represents a paginated data.
    /// </summary>
    public interface IPaginated<out T> : IPaginated
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        IEnumerable<T> Data { get; }
    }
}
