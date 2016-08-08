﻿using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;

namespace Alamut.Service
{
    /// <summary>
    /// provice base service class 
    /// with access to database as readonly repository
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class Service<TDocument> : IService<TDocument> 
        where TDocument : IEntity
    {
        public Service(IRepository<TDocument> internalRepository)
        {
            InternalRepository = internalRepository;
        }

        /// <summary>
        /// if consumer needs repository s/he must use CrudService
        /// </summary>
        internal readonly IRepository<TDocument> InternalRepository;

        protected IRepository<TDocument> BaseRepository
        {
            get { return InternalRepository; }
        }

        public IQueryRepository<TDocument> ReadOnly
        {
            get { return this.InternalRepository; }
        }
        
    }
}
