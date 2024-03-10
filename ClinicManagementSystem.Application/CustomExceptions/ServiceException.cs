using System.Net;

namespace ClinicManagementSystem.Application.CustomExceptions
{
    public class ServiceException : Exception 
    {
        public readonly HttpStatusCode _statusCode;
        public ServiceException(string msg,HttpStatusCode httpStatusCode) : base(msg) 
        {
            _statusCode = httpStatusCode;
        }
    }
}
