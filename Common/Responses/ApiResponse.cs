using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_Api.Common.Responses
{
    public class ApiResponse<T>
    {

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;


        // constructor for  response
        public ApiResponse(bool success, string message, T? data, List<string>? errors, int statusCode)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
            StatusCode = statusCode;
            Timestamp = DateTime.UtcNow;
        }
        // static method for cerateing a successful response

        // optional parameter must come after required parameters
        public static ApiResponse<T> SuccessResponse(T? data, int statusCode, string message = "")
        {
            return new ApiResponse<T>(true, message, data, null, statusCode);
        }


        // static method for Error response

        // optional parameter must come after required parameters
        public static ApiResponse<T> ErrorResponse(List<string> errors, int statusCode, string message = "")
        {
            return new ApiResponse<T>(false, message, default(T), errors, statusCode);
        }




    }
}