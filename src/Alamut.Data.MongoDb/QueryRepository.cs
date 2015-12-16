using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.NoSql;
using Alamut.Data.Paging;
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

        public object Get(DynammicCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public List<TDocument> GetAll(bool? isDeleted = null)
        {
            if (isDeleted == null && typeof(IDeletableEntity).IsAssignableFrom(typeof(TDocument)))
                isDeleted = false;

            return isDeleted == null
                ? Collection.Find(new BsonDocument()).ToList()
                : Collection.Find(new BsonDocument("IsDeleted", isDeleted.Value)).ToList();
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
            return Collection.Find(predicate).Project(projection).ToList();
        }

        public IEnumerable<object> GetMany(DynammicCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public IPaginated<TDocument> GetPaginated(PaginatedCriteria criteria, bool? isDeleted = null)
        {
            if (isDeleted == null && typeof(IDeletableEntity).IsAssignableFrom(typeof(TDocument)))
                isDeleted = false;

            var filter = isDeleted == null
                ? new BsonDocument()
                : new BsonDocument("IsDeleted", isDeleted.Value);

            using (var cursor = Collection.Find(filter)
                .Skip(criteria.StartIndex)
                .Limit(criteria.PageSize)
                .ToCursor())
            {
                return new Paginated<TDocument>(
                    cursor.ToEnumerable(),
                    Collection.Count(new BsonDocument()),
                    criteria.CurrentPage,
                    criteria.PageSize);
            }
        }

        public IPaginated<object> GetPaginated(DynamicPaginatedCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
