using log4net.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectService.Helper
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public T Data { get; set; }

        // 构造函数，接受成功状态、消息、错误码和数据  
        public ApiResponse(bool success, string message, int errorCode, T data = default(T))
        {
            Success = success;
            Message = message;
            ErrorCode = errorCode;
            Data = data;
        }

        // 静态方法，用于快速创建成功的响应  
        public static ApiResponse<T> CreateSuccessResponse(string message, T data = default(T))
        {
            return new ApiResponse<T>(true, message, 0, data); // 使用0作为默认的错误码，表示没有错误  
        }

        // 静态方法，用于快速创建失败的响应  
        public static ApiResponse<T> CreateFailedResponse(string message, int errorCode, T data = default(T))
        {
            return new ApiResponse<T>(false, message, errorCode, data);
        }

        // 提供一个默认的失败静态方法，使用默认的错误码  
        public static ApiResponse<T> CreateFailedResponse(string message, T data = default(T))
        { 
            return CreateFailedResponse(message, ApiResponseConstants.DefaultErrorCode, data);
        }
    }

    public static class ApiResponseConstants
    {
        public const int DefaultErrorCode = 400;
        public static string SuccessLabel = "操作成功";
        public static string FailedLabel = "操作失败";
    }

    public class ApiResponseFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 在这里你可以做一些请求前的处理，但通常不需要  
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                // 检查状态码来决定是成功还是失败  
                bool success = objectResult.StatusCode >= 200 && objectResult.StatusCode < 300; // 通常2xx表示成功  
                int errorCode = success ? 0 : StatusCodes.Status500InternalServerError; // 默认错误码，可以根据需要调整  
                string message = success ? ApiResponseConstants.SuccessLabel : ApiResponseConstants.FailedLabel; // 默认消息，可以根据需要调整  

                // 创建ApiResponse实例  
                var apiResponse = new ApiResponse<object>(success, message, errorCode, objectResult.Value);

                // 设置新的ObjectResult，使用ApiResponse作为数据  
                context.Result = new ObjectResult(apiResponse)
                {
                    StatusCode = objectResult.StatusCode // 保留原有的StatusCode  
                };
            }            
        }
    }
}
