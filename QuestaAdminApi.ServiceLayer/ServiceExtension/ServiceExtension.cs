using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestaAdminApi.ServiceLayer.Service;


namespace QuestaAdminApi.ServiceLayer.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection DependancyInjection(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddScoped<ICrendential, ClsCrendential>();
            service.AddScoped<IAesOperation, ClsAesOperation>();
            service.AddSingleton<IJsonConverter, ClsJsonConverter>();
            service.AddScoped<IMaster, ClsMasterData>();
            service.AddScoped<ILinkGeneration, ClsLinkGeneration>();
            service.AddScoped<IAwsConsole, ClsAwsConsole>();
            service.AddScoped<IMailSender, ClsMailSender>();
            service.AddScoped<ICandidateDetails, ClsCandidateDetails>();
            return service;
        }
    }
}
