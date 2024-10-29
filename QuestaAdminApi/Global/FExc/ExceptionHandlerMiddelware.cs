using System.Net;

namespace QuestaAdminApi.Global.FExc
{
    public class ExceptionHandlerMiddelware
    {
        private readonly RequestDelegate _next;
        private IJsonConverter _JsonConverterSvc { get; set; }
        public ExceptionHandlerMiddelware(RequestDelegate next,IJsonConverter JsonConverterSvc)
        {
            _next = next;
            _JsonConverterSvc = JsonConverterSvc;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private Task HandleExceptionMessageAsync(HttpContext context,Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode = (int)HttpStatusCode.InternalServerError;

            dynamic result = new
            {
                statuCode = statusCode,
                ErrorMessage = ex.StackTrace
            };

            string _strErrormsg = _JsonConverterSvc.JsonSerializeObject(result);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode= statusCode;

            return context.Response.WriteAsync(_strErrormsg);

        }
    }
}
