using System;
using System.Runtime.Serialization;
using Alamut.Service.Api;

namespace Alamut.Service.Helpers
{
    /// <summary>
    /// Represent Service result data structure
    /// It can be used as web service result
    /// </summary>
    /// <remarks>result for void Service</remarks>
    [DataContract]
    public class ServiceResult : IResponse
    {
        [DataMember(Name = "status")]
        public ResultStatus Status { get; set; } 

        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// return a successful ServiceResult 
        /// </summary>
        /// <returns>successful ServiceResult</returns>
        public static ServiceResult Okay()
        {
            return new ServiceResult
            {
                Status = ResultStatus.Okay
            };
        }

        /// <summary>
        /// return a ServiceResult with error status and error message
        /// </summary>
        /// <param name="message">error message</param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Error(string message)
        {
            return new ServiceResult
            {
                Status = ResultStatus.Error,
                Message = message
            };
        }

        /// <summary>
        /// return a ServiceResult from exception
        /// most inmportant exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Exception(Exception ex)
        {
            return new ServiceResult
            {
                Status = ResultStatus.Exception,
                Message = ex.GetExceptionMessages()
                //Message = ex.ToString()
            };
        }
    }

    /// <summary>
    /// provides a generic data weapper for Service result 
    /// </summary>
    /// <typeparam name="T">data type of return</typeparam>
    /// <remarks>result for not void result</remarks>
    public class ServiceResult<T> : ServiceResult
    {
        [DataMember(Name = "data")]
        public T Data { get; set; }

        /// <summary>
        /// returns a successful typed ServiceResult
        /// </summary>
        /// <param name="data">return data</param>
        /// <returns>successful ServiceResult</returns>
        public static ServiceResult<T> Okay(T data)
        {
            return new ServiceResult<T>
            {
                Status = ResultStatus.Okay,
                Data = data
            };
        }

        /// <summary>
        /// returns a typed ServiceResult with an error
        /// </summary>
        /// <param name="message">error message</param>
        /// <returns>error ServiceResult</returns>
        public new static ServiceResult<T> Error(string message)
        {
            return new ServiceResult<T>
            {
                Status = ResultStatus.Exception,
                Message = message
            };
        }
    }

    /// <summary>
    /// result type status
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// the service got the message and execute it.
        /// </summary>
        Okay = 0,

        /// <summary>
        /// the Service executed and there is some information about it.
        /// </summary>
        Information = 2,

        /// <summary>
        /// the Service executed and there is a warning.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// the commad execution has been failed with error. 
        /// </summary>
        Error = 4,

        /// <summary>
        /// the Service executed successfully 
        /// </summary>
        Successful = 5,

        /// <summary>
        /// determine that an exception occurred
        /// the exception detail provided in message
        /// </summary>
        Exception = 6,

        /// <summary>
        /// the request is not valid.
        /// </summary>
        ValidationFailed = 7
    }
}
