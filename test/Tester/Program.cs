using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alamut.Security;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var sp = Stopwatch.StartNew();

            //for (int i = 0; i < 1000; i++)
            //{

            //    //Debug.WriteLine(KeyGenerator.KeyByHash(ObjectId.GenerateNewId().GetHashCode()));
            //    Debug.WriteLine(Math.Abs(ObjectId.GenerateNewId().GetHashCode()));
            //}

            //var id = ObjectId.Parse("54be0eba5f2d130f9c508b94");
            var id = ObjectId.GenerateNewId();

            //Debug.WriteLine(Math.Abs(id.GetHashCode()));
            Debug.WriteLine(Math.Abs("54be0eba5f2d130f9c508b94".GetHashCode()));
            Debug.WriteLine(KeyGenerator.KeyByHash("54be0eba5f2d130f9c508b94"));
            Debug.WriteLine(Math.Abs("54c4c0cf5f2d1311cc4810cf".GetHashCode()));
            Debug.WriteLine(KeyGenerator.KeyByHash("54c4c0cf5f2d1311cc4810cf"));
            //Debug.WriteLine(Math.Abs("54c4c1a05f2d1311cc4810d2".GetHashCode()));

            sp.Stop();

            Console.WriteLine(sp.ElapsedMilliseconds);


            Console.ReadKey();
        }
    }
}
