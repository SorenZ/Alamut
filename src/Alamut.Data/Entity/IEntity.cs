namespace Alamut.Data.Entity
{

    /// <summary>
    /// provide by type of Entity 
    /// Id is mandetory
    /// the type of Id is string
    /// </summary>
    public interface IEntity : IEntity<string>
    { }

    /// <summary>
    /// provide by type of Entity 
    /// Id is mandetory
    /// the type of will define by type parameter
    /// </summary>
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
