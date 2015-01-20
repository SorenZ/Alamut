namespace Alamut.Service.Api
{

    /// <summary>
    /// determine the response for an specific requrest
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IResponse<TRequest> : IResponse
        where TRequest : IRequest
    { }

    /// <summary>
    /// determine the respond body
    /// </summary>
    public interface IResponse
    { }
}