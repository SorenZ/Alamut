using System;
using System.Linq;
using System.Threading;
using Alamut.Data.Entity;
using Alamut.Data.MongoDb;
using Alamut.Data.MongoDb.Mapper;
using Alamut.Data.Paging;
using Alamut.Security;
using MongoDB.Driver;

namespace Tester
{

    public class Product : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Pages : IEntity,
        IPublishableEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Basename { get; set; }
        public string Body { get; set; }
        public bool IsPublished { get; set; }

    }


    public class UniqueIdCollection : IEntity
    {
        public string Id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //MongoMapper.MapId<Pages>();

            var client = new MongoClient("mongodb://samserver");
            var database = client.GetDatabase("SamUniqueTest");


            var repo = new Repository<UniqueIdCollection>(database);

            //var list = repo.GetPaginated(new PaginatedCriteria());

            //Console.WriteLine(list.Data.First().Basename);

            
            //Console.WriteLine(Base36.Encode(ulong.Parse(DateTime.Now.ToString("yyMMddHHmmssfff"))));

            for (var i = 0; i < 1000000000; i++)
            {
                //Console.WriteLine(UniqueKeyGenerator.GenerateFromSamBegin());
                //Console.WriteLine(UniqueKeyGenerator.GenerateKeyByTick());
                //Console.WriteLine(UniqueKeyGenerator.ByHashedTick());
              //Console.WriteLine(UniqueKeyGenerator.GenerateByTime());
                repo.Create(new UniqueIdCollection {Id = UniqueKeyGenerator.GenerateByTime()});
                
            }


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
