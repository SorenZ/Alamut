﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;

namespace Alamut.Data.NoSql
{
    public interface IRepository<TDocument> : IQueryRepository<TDocument> 
        where TDocument : IEntity
    {
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
    }
}
