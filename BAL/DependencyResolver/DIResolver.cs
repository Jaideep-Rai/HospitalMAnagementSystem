using BAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using BAL.Interfaces.MedicineMaster;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {
        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddSingleton<IMedicineMasterService, MedicineMasterService>();
            return services;
        }
    }
}
