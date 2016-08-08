using System;
using System.Collections.Generic;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using AutoMapper;

namespace Alamut.Service
{
    public class HistoryService<TDocument, TRepository> : CrudService<TDocument, TRepository>, 
        IHistoryService<TDocument> 
        where TDocument : IEntity
        where TRepository : class, IRepository<TDocument>
    {
        private readonly IHistoryRepository<BaseHistory> _historyRepository;

        public HistoryService(TRepository repository, IMapper mapper, IHistoryRepository<BaseHistory> historyRepository) 
            : base(repository, mapper)
        {
            _historyRepository = historyRepository;
        }

        public virtual ServiceResult<string> Create<TModel>(TModel model, 
            string userId = null, 
            string actionDescription = null)
        {
           var result =  base.Create(model);

            if (result.Succeed)
            {
                var history = new BaseHistory
                {
                    Action = HistoryActions.Create,
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    EntityId = result.Data,
                    EntityName = typeof (TDocument).Name,
                    ModelName = typeof (TModel).Name,
                    ModelValue = model,
                };

                _historyRepository.Push(history);
            }

            return result;
        }

        public virtual ServiceResult Update<TModel>(string id, TModel model,
            string userId = null,
            string actionDescription = null)
        {
            var result = base.Update(id, model);

            if (result.Succeed)
            {
                var history = new BaseHistory
                {
                    Action = HistoryActions.Update,
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    EntityId = id,
                    EntityName = typeof(TDocument).Name,
                    ModelName = typeof(TModel).Name,
                    ModelValue = model,
                };

                _historyRepository.Push(history);
            }

            return result;
        }

        public virtual ServiceResult Delete(string id,
            string userId = null,
            string actionDescription = null)
        {
            var entity = base.ReadOnly.Get(id);

            var result = base.Delete(id);

            if (result.Succeed)
            {
                var history = new BaseHistory
                {
                    Action = HistoryActions.Delete,
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    EntityId = id,
                    EntityName = typeof(TDocument).Name,
                    ModelName = typeof(TDocument).Name,
                    ModelValue = entity,
                };

                _historyRepository.Push(history);
            }

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
