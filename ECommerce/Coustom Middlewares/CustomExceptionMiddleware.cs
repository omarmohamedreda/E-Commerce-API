using ECommerce.Domain.Exceptions;
using ECommerce.Shared.ErrorModels;

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

                // Response Object
                var response = new ErrorToReturn()
                {
                    Message = ex.Message,
                };

                #region Response Header

                // Set the response status 
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    BadRequestException badRequestException => GetBadRequestErrors(badRequestException, response),
                    _ => StatusCodes.Status500InternalServerError
                };

                #region Response Body
                response.StatusCode = context.Response.StatusCode;


                // Return Response as JSON
                await context.Response.WriteAsJsonAsync(response);
                context.Response.ContentType = "application/json";

                #endregion

                #endregion


            }
        }

        private int GetBadRequestErrors(BadRequestException exception,ErrorToReturn response)
        {
            response.Errors = exception.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }

}
