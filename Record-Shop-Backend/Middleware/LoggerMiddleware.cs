namespace Record_Shop_Backend.Middleware
{
    public class LoggerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            LogRequest(context);
            await next(context);
            LogResponse(context);
        }

        public void LogRequest(HttpContext context)
        {
            string method = context.Request.Method;
            string path = context.Request.Path;
            string dateTime = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");
            string requestInfo = $"Request received: {method} {path} at {dateTime}\n";
            File.AppendAllText("Resources/ServerLogs.txt", requestInfo);
        }

        public void LogResponse(HttpContext context)
        {
            int responseStatusCode = context.Response.StatusCode;
            string responseInfo = $"{responseStatusCode}\n";
            File.AppendAllText("Resources/ServerLogs.txt", responseInfo);
        }
    }
}
