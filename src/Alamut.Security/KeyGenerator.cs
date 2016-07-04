using System;
using System.Globalization;
using System.Threading;
using MongoDB.Bson;

namespace Alamut.Security
{
    /// <summary>
    /// simple unique key generator
    /// </summary>
    public static class UniqueKeyGenerator
    {
        /// <summary>
        /// generate base-36 key by Datetime Ticks (utc)
        /// </summary>
        /// <returns></returns>
        public static string GenerateByTick()
        {
            return Base36.Encode((ulong) DateTime.Now.Ticks);
        }

        public static string ByHashedTick()
        {
            Thread.Sleep(1);
            return Base36.Encode(Math.Abs(DateTime.Now.Ticks.GetHashCode()));
        }

        /// <summary> 
        /// generate base-36 key by string hash code
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GenerateKeyByStr(string key)
        {
            return Base36.Encode(Math.Abs(key.GetHashCode())); 
        }

        /// <summary>
        /// generate base-36 key by on object hash code
        /// </summary>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        public static string GenerateKeyByHash(int hashCode)
        {
            return Base36.Encode(Math.Abs(hashCode));
        }

        /// <summary>
        /// generate base-36 key by MongoDb object Id
        /// </summary>
        /// <returns></returns>
        public static string GenerateKeyByObjectId()
        {
            return Base36.Encode(Math.Abs(ObjectId.GenerateNewId().GetHashCode()));
        }

        

        public static string GenerateFromSamBegin()
        {
            Thread.Sleep(1);    
            var samBegin = new DateTime(2015, 01, 01);
            var elapsedTicks = DateTime.Now.Ticks - samBegin.Ticks;
            
            //return Base36.Encode((ulong)Math.Abs(elapsedTicks));
            return Base36.Encode(Math.Abs(elapsedTicks.GetHashCode()));
                
        }
    }
}
