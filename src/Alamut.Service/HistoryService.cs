using System;
using System.Collections.Generic;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using AutoMapper;

namespace Alamut.Service
{
    //[Obsolete("use full-service instead")]
    public class HistoryService<TDocument> :
        IHistoryService<TDocument> 
        where TDocument : IEntity
    {
        private readonly ICrudService<TDocument> _crudService;
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository,
            ICrudService<TDocument> crudService)
        {
            _crudService = crudService;
            _historyRepository = historyRepository;
        }

        public HistoryService(IHistoryRepository historyRepository, 
            IRepository<TDocument> repository = null,
            IMapper mapper = null)
        {
            _crudService = new CrudService<TDocument>(repository, mapper);
            _historyRepository = historyRepository;
        }

        public ServiceResult<string> Create<TModel>(TModel model, 
            string userId = null,
            string userIp = null)
        {
            var result = _crudService.Create(model);

            if (!result.Succeed) return result;
            //if (_historyRepository == null) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Create,
                UserId = userId ?? ((model is IUserEntity) ? (model as IUserEntity).UserId : null),
                CreateDate = DateTime.Now,
                EntityId = result.Data,
                EntityName = typeof(TDocument).Name,
                ModelName = typeof(TModel).Name,
                ModelValue = model,
                UserIp = userIp ?? ((model is IIpEntity) ? (model as IIpEntity).IpAddress : null)
            };

            _historyRepository.Push(history);

            return result;
        }

        public ServiceResult Update<TModel>(string id, TModel model,
            string userId = null,
            string userIp = null)
        {
            var result = _crudService.Update(id, model);

            if (!result.Succeed) return result;
            //if (_historyRepository == null) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Update,
                UserId = userId ?? ((model is IUserEntity) ? (model as IUserEntity).UserId : null),
                CreateDate = DateTime.Now,
                EntityId = id,
                EntityName = typeof(TDocument).Name,
                ModelName = typeof(TModel).Name,
                ModelValue = model,
                UserIp = userIp ?? ((model is IIpEntity) ? (model as IIpEntity).IpAddress : null)
            };

            _historyRepository.Push(history);

            return result;
        }

        public virtual ServiceResult Delete(string id,
            string userId,
            string userIp)
        {
            var entity = _crudService.ReadOnly.Get(id);

            var result = _crudService.Delete(id);

            if (!result.Succeed) return result;
            if (_historyRepository == null) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Delete,
                UserId = userId,
                CreateDate = DateTime.Now,
                EntityId = id,
                EntityName = typeof(TDocument).Name,
                ModelName = typeof(TDocument).Name,
                ModelValue = entity,
                UserIp = userIp
            };

            _historyRepository.Push(history);

            return result;

        }

        public TModel GetHistoryValue<TModel>(string historyId) where TModel : class
        {
            return _historyRepository.Pull<TModel>(historyId);
        }

        public dynamic GetHistoryValue(string historyId)
        {
            return _historyRepository.Pull(historyId);
        }

        public List<BaseHistory> GetHistories<TModel>(string entityId)
        {
            var entityName = typeof (TDocument).Name;
            var modelName = typeof (TModel).Name;

            return _historyRepository.GetMany(entityName, modelName, entityId);
        }

        public List<BaseHistory> GetHistories(string entityId)
        {
            var entityName = typeof(TDocument).Name;

            return _historyRepository.GetMany(entityName, entityId);
        }
    }
}
