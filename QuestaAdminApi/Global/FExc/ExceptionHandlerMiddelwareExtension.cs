namespace QuestaAdminApi.Global.FExc
{
    public static class ExceptionHandlerMiddelwareExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddelware>();
        }
    }
}
