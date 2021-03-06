﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Security;
using Alamut.Service.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Alamut.Service
{
    public class CrudService<TDocument> : Service<TDocument>,
        ICrudService<TDocument>
        where TDocument : IEntity
    {
        protected IMapper Mapper { get; private set; }

        public CrudService(IRepository<TDocument> repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        public virtual ServiceResult<string> Create<TModel>(TModel model)
        {
            var entity = Mapper.Map<TDocument>(model);

            if (entity is IDateEntity)
                (entity as IDateEntity).SetCreateDate();

            if (entity is ICodeEntity)
                (entity as ICodeEntity).Code = UniqueKeyGenerator.GenerateByTime();

            try
            {
                base.Repository.Create(entity);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Exception(ex);
            }

            return ServiceResult<string>.Okay(entity.Id);
        }

        public virtual ServiceResult Update<TModel>(string id, TModel model)
        {
            var entity = base.Repository.Get(id);

            if (entity == null)
                return ServiceResult.Error("There is no entity with Id : " + id, 404);

            if (entity is IDateEntity)
                (entity as IDateEntity).SetUpdateDate();

            try
            {
                base.Repository.Update(Mapper.Map(model, entity));
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Exception(ex);
            }

            return ServiceResult.Okay();
        }

        public ServiceResult UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            try
            {
                base.Repository.UpdateOne(id, memberExpression,value);
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
                base.Repository.Delete(id);
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }

            return ServiceResult.Okay("Item successfully deleted");
        }

        public virtual TResult Get<TResult>(string id)
        {
            return base.Repository.Queryable
                .Where(q => q.Id == id)
                .ProjectTo<TResult>(this.Mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids)
        {
            return base.Repository.Queryable
                .Where(q => ids.Contains(q.Id))
                .ProjectTo<TResult>(this.Mapper.ConfigurationProvider)
                .ToList();
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate)
        {
            return base.Repository.Queryable
                .Where(predicate)
                .ProjectTo<TResult>(this.Mapper.ConfigurationProvider)
                .ToList();
        }
    }

    [Obsolete("It will remove in soon, Use CrudService<TDocument> or FullService instead.")]
    public class CrudService<TDocument, TRepository> : Service<TDocument>,
        ICrudService<TDocument>
        where TDocument : IEntity
        where TRepository : class, IRepository<TDocument>
    {

        //[Obsolete("It will remove in version 3-*, user explicit repository in your onwn service")]
        //protected TRepository Repository
        //{
        //    get { return InternalRepository as TRepository; }
        //}

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

            if (entity is ICodeEntity)
                (entity as ICodeEntity).Code = UniqueKeyGenerator.GenerateByTime();

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

        public ServiceResult UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            throw new NotImplementedException();
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

        public virtual TResult Get<TResult>(string id)
        {
            return this.Repository.Queryable
                .Where(q => q.Id == id)
                .ProjectTo<TResult>(this.Mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids)
        {
            return this.Repository.Queryable
                .Where(q => ids.Contains(q.Id))
                .ProjectTo<TResult>(this.Mapper.ConfigurationProvider)
                .ToList();
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate)
        {
            return this.Repository.Queryable
                .Where(predicate)
                .ProjectTo<TResult>(this.Mapper.ConfigurationProvider)
                .ToList();
        }
    }
}