using System.Collections.Generic;

namespace Panteon.Helper
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public int StatusCode { get; set; }

        public static ServiceResponse<T> Return500(string errorMessage = "An internal server error occurred.")
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Errors = new List<string> { errorMessage },
                StatusCode = 500
            };
        }

        public static ServiceResponse<T> ReturnResultWith200(T data)
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Data = data,
                StatusCode = 200
            };
        }

        public static ServiceResponse<T> ReturnError(List<string> errorMessages, int statusCode = 400)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Errors = errorMessages,
                StatusCode = statusCode
            };
        }
    }
}
