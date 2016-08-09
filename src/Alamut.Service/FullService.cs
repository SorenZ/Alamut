using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using AutoMapper;

namespace Alamut.Service
{
    public class FullService<TDocument> : 
        IService<TDocument>,
        ICrudService<TDocument>,
        IHistoryService<TDocument> 
        where TDocument : IEntity
    {
        private readonly IRepository<TDocument> _repository;
        private readonly IMapper _mapper;
        private readonly CrudService<TDocument> _crudService;
        private readonly HistoryService<TDocument> _historyService;

        public FullService(IRepository<TDocument> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            _crudService = new CrudService<TDocument>(repository, mapper);
        }

        public FullService(IRepository<TDocument> repository,
            IMapper mapper,
            IHistoryRepository historyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            
            _crudService = new CrudService<TDocument>(repository, mapper);

            _historyService = new HistoryService<TDocument>(historyRepository, _crudService);
        }

        #region IService

        public IQueryRepository<TDocument> ReadOnly
        {
            get { return this._repository; }
        }

        protected IRepository<TDocument> BaseRepository
        {
            get { return this._repository; }
        }

        #endregion

        #region ICrudService

        protected IMapper Mapper
        {
            get { return this._mapper; }
        }

        public ServiceResult<string> Create<TModel>(TModel model)
        {
            return _crudService.Create(model);
        }

        public ServiceResult Update<TModel>(string id, TModel model)
        {
            return _crudService.Update(id, model);
        }

        public ServiceResult Delete(string id)
        {
            return _crudService.Delete(id);
        }

        public TResult Get<TResult>(string id)
        {
            return _crudService.Get<TResult>(id);
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids)
        {
            return _crudService.GetMany<TResult>(ids);
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate)
        {
            return _crudService.GetMany<TResult>(predicate);
        }

        #endregion

        #region IHistoryService

        public ServiceResult<string> Create<TModel>(TModel model, string userId = null, string userIp = null)
        {
            return _historyService.Create(model, userId, userIp);
        }

        public ServiceResult Update<TModel>(string id, TModel model, string userId = null, string userIp = null)
        {
            return _historyService.Update(id, model, userId, userIp);
        }

        public ServiceResult Delete(string id, string userId, string userIp)
        {
            return _historyService.Delete(id, userId, userIp);
        }

        public TModel GetHistoryValue<TModel>(string historyId) where TModel : class
        {
            return _historyService.GetHistoryValue<TModel>(historyId);
        }

        public dynamic GetHistoryValue(string historyId)
        {
            return _historyService.GetHistoryValue(historyId);
        }

        public List<BaseHistory> GetHistories<TModel>(string entityId)
        {
            return _historyService.GetHistories<TModel>(entityId);
        }

        public List<BaseHistory> GetHistories(string entityId)
        {
            return _historyService.GetHistories(entityId);
        }

        #endregion

    }
}
