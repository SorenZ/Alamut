using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Caching;
using Alamut.Data.Entity;
using Alamut.Data.Paging;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb
{
    /// <summary>
    /// implement full-cahced repository
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class CachedRepository<TDocument> : Repository<TDocument>
       where TDocument : IEntity
    {
        public CachedRepository(IMongoDatabase database)
            : base(database)
        { }

        // ReSharper disable once StaticMemberInGenericType
        private static readonly ObjectCache Cache = MemoryCache.Default;

        private List<TDocument> InternalSource
        {
            get
            {
                if (Cache.Contains(typeof (TDocument).Name))
                    return Cache.Get(typeof (TDocument).Name) as List<TDocument>;

                var data = base.GetAll();
                Cache.Add(typeof (TDocument).Name,
                    data,
                    new CacheItemPolicy
                    {SlidingExpiration = TimeSpan.FromMinutes(30)});

                return data;
            }
        }

        private static void RefreshCache()
        {
            Cache.Remove(typeof (TDocument).Name);
        }

        public override IQueryable<TDocument> Queryable {
            get { return InternalSource.AsQueryable(); } 
        }

        public override TDocument Get(string id)
        {
            return InternalSource.FirstOrDefault(q => q.Id == id);
        }

        public override TDocument Get(Expression<Func<TDocument, bool>> predicate)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override TResult Get<TResult>(string id, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override List<TDocument> GetAll()
        {
            return InternalSource;
        }

        public override List<TResult> GetAll<TResult>(Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override List<TDocument> GetMany(IEnumerable<string> ids)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override List<TResult> GetMany<TResult>(IEnumerable<string> ids, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override IPaginated<TDocument> GetPaginated(PaginatedCriteria criteria = null)
        {
            throw new NotImplementedException("use Queryable");
        }

        public override void Create(TDocument entity)
        {
            base.Create(entity);
            RefreshCache();
        }

        public override void AddRange(IEnumerable<TDocument> list)
        {
            base.AddRange(list);
            RefreshCache();
        }

        public override void Update(TDocument entity)
        {
            base.Update(entity);
            RefreshCache();
        }

        public override void UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            base.UpdateOne(id, memberExpression, value);
            RefreshCache();
        }

        public override void UpdateOne<TFilter, TField>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            base.UpdateOne<TFilter,TField>(filterExpression, memberExpression, value);
            RefreshCache();
        }

        public override void GenericUpdate(string id, Dictionary<string, dynamic> fieldset)
        {
            base.GenericUpdate(id, fieldset);
            RefreshCache();
        }

        public override void AddToList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            base.AddToList(id, memberExpression, value);
            RefreshCache();
        }

        public override void RemoveFromList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            base.RemoveFromList(id, memberExpression, value);
            RefreshCache();
        }

        public override void Delete(string id)
        {
            base.Delete(id);
            RefreshCache();
        }

        public override void DeleteMany(Expression<Func<TDocument, bool>> predicate)
        {
            base.DeleteMany(predicate);
            RefreshCache();
        }

        public override void SetDeleted(string id)
        {
            base.SetDeleted(id);
            RefreshCache();
        }
        
    }
}
