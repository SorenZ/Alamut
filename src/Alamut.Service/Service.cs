using System;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;

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

        public ServiceResult Delete(string id)
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
