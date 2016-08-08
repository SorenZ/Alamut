using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Service.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
        private readonly IHistoryRepository _historyRepository;

        public FullService(IRepository<TDocument> repository,
            IMapper mapper,
            IHistoryRepository historyRepository = null)
        {
            _repository = repository;
            _mapper = mapper;
            _historyRepository = historyRepository;
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

        ServiceResult<string> ICrudService<TDocument>.Create<TModel>(TModel model)
        {
            var entity = _mapper.Map<TDocument>(model);

            if (entity is IDateEntity)
                (entity as IDateEntity).SetCreateDate();

            //if(entity is ICodeEntity)
            //    (entity as ICodeEntity).Code = UniqueKey

            try
            {
                this._repository.Create(entity);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Exception(ex);
            }

            return ServiceResult<string>.Okay(entity.Id);
        }

        ServiceResult ICrudService<TDocument>.Update<TModel>(string id, TModel model)
        {
            var entity = this._repository.Get(id);

            if (entity == null)
                return ServiceResult.Error("There is no entity with Id : " + id, 404);

            if (entity is IDateEntity)
                (entity as IDateEntity).SetUpdateDate();

            try
            {
                this._repository.Update(_mapper.Map(model, entity));
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }

            return ServiceResult.Okay();
        }

        ServiceResult ICrudService<TDocument>.Delete(string id)
        {
            if (id == null)
                return ServiceResult.Error("Id could not be null");

            try
            {
                this._repository.Delete(id);
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }

            return ServiceResult.Okay("Item successfully deleted");
        }

        public TResult Get<TResult>(string id)
        {
            return this._repository.Queryable
                .Where(q => q.Id == id)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids)
        {
            return this._repository.Queryable
                .Where(q => ids.Contains(q.Id))
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate)
        {
            return this._repository.Queryable
                .Where(predicate)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToList();
        }

        #endregion

        #region IHistoryService

        public ServiceResult<string> Create<TModel>(TModel model, string userId = null, string userIp = null)
        {
            var result = (this as ICrudService<TDocument>).Create(model);

            if (!result.Succeed) return result;
            if (_historyRepository == null) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Create,
                UserId = userId ?? ((model is IUserEntity) ? (model as IUserEntity).UserId: null),
                CreateDate = DateTime.Now,
                EntityId = result.Data,
                EntityName = typeof (TDocument).Name,
                ModelName = typeof (TModel).Name,
                ModelValue = model,
                UserIp = userIp ?? ((model is IIpEntity) ? (model as IIpEntity).IpAddress : null)
            };

            _historyRepository.Push(history);

            return result;
        }

        public ServiceResult Update<TModel>(string id, TModel model, string userId = null, string userIp = null)
        {
            var result = (this as ICrudService<TDocument>).Update(id, model);

            if (!result.Succeed) return result;
            if (_historyRepository == null) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Update,
                UserId = userId ?? ((model is IUserEntity) ? (model as IUserEntity).UserId: null),
                CreateDate = DateTime.Now,
                EntityId = id,
                EntityName = typeof (TDocument).Name,
                ModelName = typeof (TModel).Name,
                ModelValue = model,
                UserIp = userIp ?? ((model is IIpEntity) ? (model as IIpEntity).IpAddress : null)
            };

            _historyRepository.Push(history);

            return result;
        }

        public ServiceResult Delete(string id, string userId, string userIp)
        {
            var entity = this.ReadOnly.Get(id);

            var result = (this as ICrudService<TDocument>).Delete(id);

            if (!result.Succeed) return result;
            if (_historyRepository == null) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Delete,
                UserId = userId,
                CreateDate = DateTime.Now,
                EntityId = id,
                EntityName = typeof (TDocument).Name,
                ModelName = typeof (TDocument).Name,
                ModelValue = entity,
                UserIp = userIp
            };

            _historyRepository.Push(history);

            return result;

        }

        public TModel GetHistoryValue<TModel>(string historyId) where TModel : class
        {
            if (_historyRepository == null) throw new Exception("IHistoryRepository not provided.");

            return _historyRepository.Pull<TModel>(historyId);
        }

        public dynamic GetHistoryValue(string historyId)
        {
            if (_historyRepository == null) throw new Exception("IHistoryRepository not provided.");

            return _historyRepository.Pull(historyId);
        }

        public List<BaseHistory> GetHistories<TModel>(string entityId)
        {
            if (_historyRepository == null) throw new Exception("IHistoryRepository not provided.");

            var entityName = typeof (TDocument).Name;
            var modelName = typeof (TModel).Name;

            return _historyRepository.GetMany(entityName, modelName, entityId);
        }

        public List<BaseHistory> GetHistories(string entityId)
        {
            if (_historyRepository == null) throw new Exception("IHistoryRepository not provided.");

            var entityName = typeof (TDocument).Name;

            return _historyRepository.GetMany(entityName, entityId);
        }

        #endregion

    }
}
