namespace Alamut.Service.Api
{

    /// <summary>
    /// determine the response for an specific requrest
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IResponse<TRequest> where TRequest : IRequest, 
        IResponse
    { }

    /// <summary>
    /// determine the respond body
    /// </summary>
    public interface IResponse
    { }
}