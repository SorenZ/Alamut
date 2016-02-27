using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Alamut.Data.Entity;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb
{
    /// <summary>
    /// implement full-cahced repository
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class CachedRepository<TDocument> : IRepository<TDocument>
        where TDocument : IEntity
    {
        private readonly IList<TDocument> _internalSource;
        private readonly IRepository<TDocument> _internalRepository;
        
        public CachedRepository(IMongoDatabase database)
        {
            this._internalSource = new List<TDocument>();
            _internalRepository = new Repository<TDocument>(database);
        }

        public IQueryable<TDocument> Queryable {
            get { return _internalSource.AsQueryable(); } 
        }

        public TDocument Get(string id)
        {
            return this._internalSource.FirstOrDefault(q => q.Id == id);
        }

        public TDocument Get(Expression<Func<TDocument, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TResult Get<TResult>(string id, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException();
        }

        public TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException();
        }

        public List<TDocument> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TResult> GetAll<TResult>(Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException();
        }

        public List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<TDocument> GetMany(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException();
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException();
        }

        public IPaginated<TDocument> GetPaginated(PaginatedCriteria criteria = null)
        {
            throw new NotImplementedException();
        }

        public void Create(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TDocument entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            throw new NotImplementedException();
        }

        public void UpdateOne<TFilter, TField>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            throw new NotImplementedException();
        }

        public void GenericUpdate(string id, Dictionary<string, dynamic> fieldset)
        {
            throw new NotImplementedException();
        }

        public void AddToList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void SetDeleted(string id)
        {
            throw new NotImplementedException();
        }
    }
}
