using BAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using BAL.Interfaces.MedicineMaster;
using BAL.Interfaces.UserMaster;
using BAL.Operations.UserMaster;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {
        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddSingleton<IMedicineMasterService, MedicineMasterService>();
            services.AddSingleton<IUserMasterService, UserMasterService>();
            return services;
        }
    }
}
