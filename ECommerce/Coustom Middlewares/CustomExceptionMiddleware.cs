using ECommerce.Domain.Exceptions;

namespace ECommerce.Coustom_Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate Next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);


                // check for for 404 not found (endpoint not found)
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    
                    // Response Object
                    var response = new
                    {
                        StatusCode =StatusCodes.Status404NotFound,
                        Message = $"End Point {context.Request.Path} is not Found",
                    };

                    // Return Response as JSON
                    await context.Response.WriteAsJsonAsync(response);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                #region Response Header

                // Set the response status 
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };


                // code and content type 
                context.Response.ContentType = "application/json";
                #endregion

                #region Response Body

                // Response Object
                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                };

                // Return Response as JSON
                await context.Response.WriteAsJsonAsync(response);

                #endregion
            }
        }
    }

}
