using System;
using Alamut.Data.Entity;
using Alamut.Data.MongoDb;
using Alamut.Data.MongoDb.Mapper;
using MongoDB.Driver;

namespace Tester
{

    public class Product : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AutoMapper.MapId<Product>();

            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("test");
            

            var repo = new Repository<Product>(database);


            var product = new Product
            {
                Name = "product 2"
            };

            repo.Create(product);

            Console.WriteLine(product.Id);



            Console.ReadLine();
        }

        //static void Main(string[] args)
        //{
        //    var sp = Stopwatch.StartNew();

        //    //for (int i = 0; i < 1000; i++)
        //    //{

        //    //    //Debug.WriteLine(KeyGenerator.KeyByHash(ObjectId.GenerateNewId().GetHashCode()));
        //    //    Debug.WriteLine(Math.Abs(ObjectId.GenerateNewId().GetHashCode()));
        //    //}

        //    //var id = ObjectId.Parse("54be0eba5f2d130f9c508b94");
        //    var id = ObjectId.GenerateNewId();

        //    //Debug.WriteLine(Math.Abs(id.GetHashCode()));
        //    //Debug.WriteLine(Math.Abs("54be0eba5f2d130f9c508b94".GetHashCode()));
        //    //Debug.WriteLine(KeyGenerator.KeyByHash("54be0eba5f2d130f9c508b94"));
        //    //Debug.WriteLine(Math.Abs("54c4c0cf5f2d1311cc4810cf".GetHashCode()));
        //    //Debug.WriteLine(KeyGenerator.KeyByHash("54c4c0cf5f2d1311cc4810cf"));
        //    //Debug.WriteLine(Math.Abs("54c4c1a05f2d1311cc4810d2".GetHashCode()));

        //    sp.Stop();

        //    Console.WriteLine(sp.ElapsedMilliseconds);


        //    Console.ReadKey();
        //}
    }
}
