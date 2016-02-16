using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Structure;

namespace Alamut.Data.Service
{
    /// <summary>
    /// provide base CRUD service contract 
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public interface ICrudService<TDocument> : IService<TDocument> 
        where TDocument : IEntity 
    {
        /// <summary>
        /// create an item by mapping model into entity and 
        /// add it in database.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult<string> Create<TModel>(TModel model);

        /// <summary>
        /// update an item by id
        /// mapping model into entity (use new properties in model & old properties in entity)
        /// update database
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult Update<TModel>(string id, TModel model);

        /// <summary>
        /// delete item by Id
        /// </summary>
        /// <param name="id">entity or document Id</param>
        /// <returns></returns>
        ServiceResult Delete(string id);
    }
}