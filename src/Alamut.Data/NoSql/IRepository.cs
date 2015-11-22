using Alamut.Data.Entity;

namespace Alamut.Data.NoSql
{
    public interface IRepository<TEntity> : IRepository<TEntity,string>
        where TEntity : IEntity
    {
         
    }


    public interface IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {

    }
}
