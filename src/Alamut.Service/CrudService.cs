using System;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Service.Helpers;
using AutoMapper;

namespace Alamut.Service
{
    public class CrudService<TDocument, TRepository> : Service<TDocument>,
        ICrudService<TDocument>
        where TDocument : IEntity
        where TRepository : class, IRepository<TDocument>
    {
        protected TRepository Repository
        {
            get { return InternalRepository as TRepository; }
        }

        protected IMapper Mapper { get; private set; }

        public CrudService(TRepository repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        public virtual ServiceResult<string> Create<TModel>(TModel model)
        {
            var entity = Mapper.Map<TDocument>(model);

            if (entity is IDateEntity)
                (entity as IDateEntity).SetCreateDate();

            try
            {
                this.Repository.Create(entity);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Exception(ex);
            }

            return ServiceResult<string>.Okay(entity.Id);
        }

        public virtual ServiceResult Update<TModel>(string id, TModel model)
        {
            var entity = this.Repository.Get(id);

            if (entity == null)
                return ServiceResult.Error("There is no entity with Id : " + id, 404);

            if(entity is IDateEntity)
                (entity as IDateEntity).SetUpdateDate();

            try
            {
                this.Repository.Update(Mapper.Map(model, entity));
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Exception(ex);
            }

            return ServiceResult.Okay();
        }

        public virtual ServiceResult Delete(string id)
        {
            if (id == null)
                return ServiceResult.Error("Id could not be null");

            try
            {
                this.Repository.Delete(id);
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }

            return ServiceResult.Okay("Item successfully deleted");
        }
    }
}