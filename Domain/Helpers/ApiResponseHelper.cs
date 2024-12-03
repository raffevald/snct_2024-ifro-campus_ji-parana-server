using Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace Domain.Helpers
{
    public static class ApiResponseHelper
    {
        public static ApiResponse<T> Ok<T>(T data, string message = "Operation successful.")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = StatusCodes.Status200OK
            };
        }

        //public static PagedResponse<T> OkPaged<T>(
        //    IEnumerable<T> data,
        //    Pagination pagination,
        //    string message = "Operation successful.")
        //{
        //    return new PagedResponse<T>
        //    {
        //        Data = data,
        //        Pagination = pagination,
        //        Message = message,
        //        Success = true,
        //        StatusCode = StatusCodes.Status200OK,
        //    };
        //}

        public static ApiResponse<T> Unauthorized<T>(string message = "Unauthorized access.", string errorMessage = "")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                ErrorMessage = errorMessage,
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        public static ApiResponse<T> NotFound<T>(string message = "Resource not found.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        public static ApiResponse<T> BadRequest<T>(string message = "Invalid request.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        public static ApiResponse<T> InternalServerError<T>(string message = "An error occurred.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        //public static PagedResponse<T> InternalServerErrorPaged<T>(string message = "An error occurred.")
        //{
        //    return new PagedResponse<T>
        //    {
        //        Success = false,
        //        Message = message,
        //        StatusCode = StatusCodes.Status500InternalServerError
        //    };
        //}
    }
}
