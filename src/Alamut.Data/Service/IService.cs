﻿using System.Linq;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Structure;

namespace Alamut.Data.Service
{
    /// <summary>
    /// provide base service contract
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public interface IService<TDocument>  where TDocument : IEntity
    {
        /// <summary>
        /// provide a readonly repository in order to facilitate 
        /// access to data(readonly) from who that access to the service.
        /// </summary>
        IQueryRepository<TDocument> ReadOnly { get; }

        /// <summary>
        /// delete item by Id
        /// </summary>
        /// <param name="id">entity or document Id</param>
        /// <returns></returns>
        ServiceResult Delete(string id);
    }
}
