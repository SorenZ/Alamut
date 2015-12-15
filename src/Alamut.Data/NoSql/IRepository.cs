using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Paging;

namespace Alamut.Data.NoSql
{
    public interface IRepository<TDocument> where TDocument : IEntity
    {
        /// <summary>
        /// Creates a queryable source of documents
        /// </summary>
        IQueryable<TDocument> Queryable { get; }

        /// <summary>
        /// create an item
        /// </summary>
        /// <param name="entity"></param>
        void Create(TDocument entity);


        /// <summary>
        /// update item total value
        /// </summary>
        /// <param name="entity"></param>
        void Update(TDocument entity);

        /// <summary>
        /// update an item (one field) by expression member selector by id
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        /// <remarks>
        /// Even if multiple documents match the filter, only one will be updated because we used UpdateOne
        /// </remarks>
        void UpdateOne<TField>(string id, 
            Expression<Func<TDocument, TField>> memberExpression, 
            TField value);

        /// <summary>
        /// update an item (one field) by expression member selector (select item by predicate)
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        /// <remarks>
        /// Even if multiple documents match the filter, only one will be updated because we used UpdateOne
        /// </remarks>
        void UpdateOne<TFilter, TField>(Expression<Func<TDocument, bool>> filterExpression, 
            Expression<Func<TDocument, TField>> memberExpression, TField value);

        /// <summary>
        /// update fieldset in the databse by provided id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fieldset"></param>
        void GenericUpdate(string id, Dictionary<string, dynamic> fieldset);

        
        /// <summary>
        /// add an item to a list (if not exist)
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        void AddToList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value);

        /// <summary>
        /// remove an item from a list (all item if same)
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        void RemoveFromList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value);


        /// <summary>
        /// Deletes an item by id.
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        /// <summary>
        /// Deletes multiple documents.
        /// </summary>
        /// <param name="predicate">represent expression to filter delete</param>
        void DeleteMany(Expression<Func<TDocument, bool>> predicate);


        /// <summary>
        /// set is deleted to true by id
        /// </summary>
        /// <param name="id"></param>
        void SetDeleted(string id);

        /// <summary>
        /// get an item by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TDocument Get(string id);

        /// <summary>
        /// get an item by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TDocument Get(Expression<Func<TDocument, bool>> predicate);

        /// <summary>
        /// get an item (selected fields bye projection) by id
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="id"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        TResult Get<TResult>(string id, Expression<Func<TDocument, TResult>> projection);

        /// <summary>
        /// get an item (selected fields bye projection) by predicate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate, 
            Expression<Func<TDocument, TResult>> projection);

        /// <summary>
        /// get one item by filters
        /// support by Id query
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        object Get(DynammicCriteria criteria);

        /// <summary>
        /// get all items 
        /// </summary>
        /// <param name="isDeleted">
        /// could be true, false, null
        /// null -> not important 
        /// </param>
        /// <returns></returns>
        List<TDocument> GetAll(bool? isDeleted = null);


        /// <summary>
        /// get a list of items by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate);

        /// <summary>
        /// get a list of items by ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<TDocument> GetMany(IEnumerable<string> ids);

        /// <summary>
        /// get a list of items (selected fields) by predicate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate, 
            Expression<Func<TDocument, TResult>> projection);

        /// <summary>
        /// get items (fields or all of them) by filters 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<object> GetMany(DynammicCriteria criteria);

        /// <summary>
        /// get items paginated by criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        IPaginated<TDocument> GetPaginated(PaginatedCriteria criteria, bool? isDeleted = null);

        /// <summary>
        /// get items paginated and filterd and sorted by criteria(s)
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IPaginated<object> GetPaginated(DynamicPaginatedCriteria criteria);
    }


    //public interface IRepository<TEntity, TKey>
    //    where TEntity : IEntity<TKey>
    //{

    //}
}
