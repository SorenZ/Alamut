using System;

namespace Alamut.Helpers.Object
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// determine wheather object must not be null
        /// otherwise throw an exception
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        public static void NotNull(this object obj, string message = "")
        {
            if (obj == null)
                throw new NullReferenceException(message);
        }
    }
}
