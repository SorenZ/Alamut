using Alamut.Data.Entity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Alamut.Data.MongoDb.Mapper
{
    public static class AutoMapper
    {
        public static void MapId<TEntity>() where TEntity : IEntity
        {
            BsonClassMap.RegisterClassMap<TEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            });
        }
    }
}
