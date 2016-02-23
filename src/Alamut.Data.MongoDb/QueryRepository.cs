using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb
{
    public class QueryRepository<TDocument> : IQueryRepository<TDocument> where TDocument : IEntity
    {
        protected readonly IMongoCollection<TDocument> Collection;

        public QueryRepository(IMongoDatabase database)
        {
            Collection = database.GetCollection<TDocument>(typeof (TDocument).Name);
        }

        public IQueryable<TDocument> Queryable
        {
            get { return Collection.AsQueryable(); }
        }

        public TDocument Get(string id)
        {
            return Collection.Find(m => m.Id == id).FirstOrDefault();
        }

        public TDocument Get(Expression<Func<TDocument, bool>> predicate)
        {
            return Collection.Find(predicate).FirstOrDefault();
        }

        public TResult Get<TResult>(string id, Expression<Func<TDocument, TResult>> projection)
        {
            return Collection.Find(m => m.Id == id)
                .Project(projection)
                .FirstOrDefault();
        }

        public TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate,
            Expression<Func<TDocument, TResult>> projection)
        {
            return Collection.Find(predicate)
                .Project(projection)
                .FirstOrDefault();
        }

        public List<TDocument> GetAll()
        {
            return Collection.Find(new BsonDocument()).ToList();
        }

        public List<TResult> GetAll<TResult>(Expression<Func<TDocument, TResult>> projection)
        {
            return Collection.Find(new BsonDocument())
                .Project(projection)
                .ToList();
        }

        public List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate)
        {
            return Collection.Find(predicate).ToList();
        }

        public List<TDocument> GetMany(IEnumerable<string> ids)
        {
            return Collection.Find(q => ids.Contains(q.Id)).ToList();
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate,
            Expression<Func<TDocument, TResult>> projection)
        {
            return Collection
                .Find(predicate)
                .Project(projection)
                .ToList();
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids, Expression<Func<TDocument, TResult>> projection)
        {
            return Collection
                .Find(q => ids.Contains(q.Id))
                .Project(projection)
                .ToList();
        }


        public IPaginated<TDocument> GetPaginated(PaginatedCriteria criteria = null)
        {
            var internalCriteria = criteria ?? new PaginatedCriteria();

            var query = Collection.Find(new BsonDocument())
                .Skip(internalCriteria.StartIndex)
                .Limit(internalCriteria.PageSize);

            return new Paginated<TDocument>(query.ToEnumerable(),
                query.Count(),
                internalCriteria.CurrentPage,
                internalCriteria.PageSize);
        }

    }
}
