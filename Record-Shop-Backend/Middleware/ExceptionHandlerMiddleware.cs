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
            catch (ArgumentNullException albumNotFound)
            {
                await HandleAtgumentNullException(context, albumNotFound);
            }
            catch (ArgumentException albumExists)
            {
                await HandleArgumentException(context, albumExists);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleAtgumentNullException(HttpContext context, Exception albumNotFound)
        {
            context.Response.StatusCode = StatusCodes.Status204NoContent;
            await context.Response.WriteAsJsonAsync(new { message = "No album exists on the database with the given ID" });
        }
        private async Task HandleArgumentException(HttpContext context, Exception albumExists)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(new { message = "This album already exists on the database" });
        }
        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "Database Connection isn't available" });
        }
    }
}
