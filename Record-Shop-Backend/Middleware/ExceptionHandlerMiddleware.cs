namespace Record_Shop_Backend.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception ex)
        {
            Console.WriteLine("Unknown Error");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "Database Connection isn't available" });
        }
    }
}
