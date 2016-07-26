using System;
using Alamut.Data.Entity;
using Alamut.Data.MongoDb;
using Alamut.Data.MongoDb.BsonSerializer;
using Alamut.Data.MongoDb.Mapper;
using Alamut.Service;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
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

    public class CreatePagesVm
    {
        public string Title { get; set; }
        public string Basename { get; set; }
    }

    public class UpdatePagesVm
    {
        public string Title { get; set; }
        public string Basename { get; set; }
    }


    public class UniqueIdCollection : IEntity
    {
        public string Id { get; set; }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            MongoMapper.MapId<Pages>();
            //MongoMapper.MapId<BaseHistory>();
            BsonClassMap.RegisterClassMap<BaseHistory>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.MapProperty(m => m.ModelValue).SetSerializer(new JObjectSerializer());
            });

            var mapper = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<CreatePagesVm, Pages>();
                configuration.CreateMap<UpdatePagesVm, Pages>();
            })
                .CreateMapper();

            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("SAM");

            var repo = new Repository<Pages>(database);
            var historyRepo = new HistoryRepository<BaseHistory>(database);

            var service = new CrudServiceWithHistory<Pages, Repository<Pages>>(repo, mapper, historyRepo);

            //var result = service.Create(new CreatePagesVm {Basename = "basename", Title = "creation"});
            //service.Update(result.Data, new UpdatePagesVm { Basename = "new base", Title = "new title" });
            //service.Delete(result.Data);

            //var histories = service.GetHistories<UpdatePagesVm>();

            //foreach (var baseHistory in histories)
            //{   
            //    Console.WriteLine(baseHistory.Action + " " + baseHistory.ModelName);
            //}

            var history = service.GetHistory("57970510486ff5154c2549ea");
            Console.WriteLine(history.Title + " " + history.Basename);

            

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
