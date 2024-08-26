using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectService.Helper
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // 记录异常信息到日志  
            LogManager.Log.Error("An unhandled exception occurred.", context.Exception);

            var apiResponse = ApiResponse<object>.CreateFailedResponse(string.Format("服务器内部错误:{0}", context.Exception.Message), 500, null); // 使用默认的500状态码和错误消息

            // 写入错误响应到HTTP响应体  
            context.Result = new ObjectResult(apiResponse)
            {
                
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // 标记异常已处理，防止被后续中间件或过滤器再次处理  
            context.ExceptionHandled = true;
        }
    }
}
