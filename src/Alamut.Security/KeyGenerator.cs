using System;

namespace Alamut.Security
{
    /// <summary>
    /// simple unique key generator
    /// </summary>
    public static class KeyGenerator
    {
        /// <summary>
        /// generate base-36 key by Datetime Ticks (utc)
        /// </summary>
        /// <returns></returns>
        public static string KeyByTick()
        {
            return Base36.Encode((ulong) DateTime.UtcNow.Ticks);
        }

        /// <summary>
        /// generate base-36 key by string hash code
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string KeyByStr(string key)
        {
            return Base36.Encode(Math.Abs(key.GetHashCode()));
        }

        /// <summary>
        /// generate base-36 key by on object hash code
        /// </summary>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        public static string KeyByHash(int hashCode)
        {
            return Base36.Encode(Math.Abs(hashCode));
        }
    }
}
