using System.Linq;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;

namespace Alamut.Service
{
    /// <summary>
    /// provice base service class 
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class Service<TDocument> : IService<TDocument> where TDocument : IEntity
    {
        protected readonly IRepository<TDocument> Repository;

        public Service(IRepository<TDocument> repository)
        {
            Repository = repository;
        }

        public IQueryRepository<TDocument> ReadOnly
        {
            get { return this.Repository; }
        }

        public IQueryable<TDocument> Query
        {
            get { return this.Repository.Queryable; }
        }
    }
}
