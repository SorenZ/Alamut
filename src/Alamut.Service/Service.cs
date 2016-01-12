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
        private readonly IRepository<TDocument> _repository;

        public Service(IRepository<TDocument> repository)
        {
            _repository = repository;
        }

        public IQueryRepository<TDocument> ReadOnly
        {
            get { return this._repository; }
        }
    }
}
