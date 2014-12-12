namespace Alamut.Data.Linq
{
    /// <summary>
    /// Represents a sort description.
    /// </summary>
    public class SortDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortDescription" /> class.
        /// </summary>
        /// <param name="propertyName"> Name of the property. </param>
        /// <param name="direction"> The direction. </param>
        public SortDescription(string propertyName, SortDirection direction)
        {
            this.PropertyName = propertyName;
            this.Direction = direction;
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value> The direction. </value>
        public SortDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value> The name of the property. </value>
        public string PropertyName { get; set; }
    }

    /// <summary>
    /// Specifies the direction of a sort operation.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending 
        /// </summary>
        Ascending,

        /// <summary>
        /// Descending
        /// </summary>
        Descending
    }
}
