namespace Alamut.Helpers.Enum
{
    public static class StringExtensions
    {
        /// <summary>
        /// parse enum type by string name
        /// </summary>
        /// <typeparam name="T">the enum type</typeparam>
        /// <param name="enumString">the enum name</param>
        /// <returns>enum item</returns>
        public static T ToEnum<T>(this string enumString)
        {
            return (T)System.Enum.Parse(typeof(T), enumString);
        }
    }
}
