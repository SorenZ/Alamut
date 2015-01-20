namespace Alamut.Service.Api
{
    /// <summary>
    /// determine the request body in api negotiation
    /// </summary>
    /// <typeparam name="TRespond"></typeparam>
    public interface IRequest<TRespond> : IRequest
    { }

    /// <summary>
    /// determine the request that shouldn't have any respond.
    /// </summary>
    public interface IRequest
    { }
}
