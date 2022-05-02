using Microsoft.Extensions.DependencyInjection;
using BAL.Interfaces.MedicineMaster;
using BAL.Interfaces.UserMaster;
using BAL.Operations.UserMaster;
using BAL.Operations.BedMaster;
using BAL.Interfaces.BedMaster;
using BAL.Interfaces.PatientMaster;
using BAL.Operations;
using BAL.Interfaces.BillMaster;
using BAL.Operations.BillMaster;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {
        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<IMedicineMasterService, MedicineMasterService>();
            services.AddScoped<IUserMasterService, UserMasterService>();
            services.AddScoped<IBedMasterService, BedMasterService>();
            services.AddScoped<IPatientMasterService, PatientMasterService>();
            services.AddScoped<IBillMaster, BillMasterService>();
            return services;
        }
    }
}
