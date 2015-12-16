﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.NoSql;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb
{
    public class Repository<TDocument> : QueryRepository<TDocument>,
        IRepository<TDocument> 
        where TDocument : IEntity
    {
        public Repository(IMongoDatabase database) : base(database)
        { }

        public void Create(TDocument entity)
        {
            
            Collection.InsertOne(entity);
        }

        public void Update(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateOne<TField>(string id, 
            Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .Set(memberExpression, value);

            Collection.UpdateOne(filter, update);
        }

        public void UpdateOne<TFilter, TField>(Expression<Func<TDocument, bool>> predicate, 
            Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            var update = Builders<TDocument>.Update
                .Set(memberExpression, value);

            Collection.UpdateOne(predicate, update);
        }

        public void GenericUpdate(string id, Dictionary<string, dynamic> fieldset)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var updateList = new List<UpdateDefinition<TDocument>>();

            foreach (var field in fieldset)
            {
                if (field.Value is IEnumerable && !(field.Value is string))
                    updateList.Add(Builders<TDocument>.Update.Set(field.Key, ((IEnumerable) field.Value).ToBson()));
                else
                    updateList.Add(Builders<TDocument>.Update.Set(field.Key, (BsonValue) field.Value ?? BsonNull.Value));
            }

            Collection.UpdateOne(filter, Builders<TDocument>.Update.Combine(updateList));
        }

        public void AddToList<TValue>(string id, 
            Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .AddToSet(memberExpression, value);

            Collection.UpdateOne(filter, update);
        }

        public void RemoveFromList<TValue>(string id, 
            Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .Pull(memberExpression, value);

            Collection.UpdateOne(filter, update);
        }

        public void Delete(string id)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            Collection.DeleteOne(filter);
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> predicate)
        {
            Collection.DeleteMany(predicate);
        }

        public void SetDeleted(string id)
        {
            Collection.UpdateOne(q => q.Id == id,
                new BsonDocument("$set", new BsonDocument(EntitySsot.IsDeleted, true)));
        }
    }
}