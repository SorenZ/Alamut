using Alamut.Data.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Alamut.Data.MongoDb.Mapper
{
    public static class MongoMapper
    {
        public static void MapId<TEntity>() where TEntity : IEntity
        {
            BsonClassMap.RegisterClassMap<TEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                //cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            });
        }
    }
}
